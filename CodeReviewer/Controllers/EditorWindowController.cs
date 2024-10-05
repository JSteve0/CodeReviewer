using System.Windows;
using CodeReviewer.Logging;
using CodeReviewer.Models.Languages;
using Microsoft.Web.WebView2.Wpf;
using Wpf.Ui.Appearance;

namespace CodeReviewer.Controllers;

public class EditorWindowController(WebView2 webView2) : IEditorWindowController {

    private const string EditorContainerSelector = "#root";
    private const string EditorObject = "wpfUiMonacoEditor";
    
    private readonly ILogger _logger = Logger.Instance;

    /// <summary>
    ///     Creates a new async task to initialize the Monaco Editor.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CreateAsync() {
        const string script = $$"""
                                const {{EditorObject}} = monaco.editor.create(document.querySelector('{{EditorContainerSelector}}'));window.onresize = () => {{{EditorObject}}.layout()};
                                """;
        await ExecuteScriptWithLoggingAsync(script);
    }

    /// <summary>
    ///     Dispatches a script to the UI thread for execution.
    /// </summary>
    /// <param name="script">The script to be executed.</param>
    public void DispatchScript(string script) {
        RunOnUIThreadAsync(() => ExecuteScriptWithLoggingAsync(script));
    }

    /// <summary>
    ///     Retrieves the content of the Monaco Editor asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result represents the content of the Monaco Editor.</returns>
    public async Task<string> GetContent() {
        try {
            return await webView2.ExecuteScriptAsync($"{EditorObject}.getValue()");
        }
        catch (Exception ex) {
            _logger.LogError($"Error retrieving content: {ex.Message}");
            return string.Empty;
        }
    }

    /// <summary>
    ///     Sets the content of the Monaco Editor.
    /// </summary>
    /// <param name="contents">The content to be set in the editor.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SetContentAsync(string contents) {
        string sanitizedContents = Utils.Utils.EncodeJsString(contents);
        await ExecuteScriptWithLoggingAsync($"{EditorObject}.setValue({sanitizedContents});", false);
    }

    /// <summary>
    ///     Sets the language of the Monaco Editor asynchronously.
    /// </summary>
    /// <param name="programmingLanguage">The programming language to set. If null, the language will be set to "plaintext".</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SetLanguageAsync(IProgrammingLanguage? programmingLanguage) {
        string languageId = programmingLanguage?.MonacoName ?? programmingLanguage?.Name ?? "plaintext";

        string script = "monaco.editor.setModelLanguage(" + EditorObject + $".getModel(), \"{languageId.ToLower()}\");";
        await ExecuteScriptWithLoggingAsync(script);
    }

    /// <summary>
    ///     Sets the theme of the Monaco Editor asynchronously.
    /// </summary>
    /// <param name="appApplicationTheme">The application theme to be set.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SetThemeAsync(ApplicationTheme appApplicationTheme) {
        const string uiThemeName = "wpf-ui-app-theme";

        // TODO: Add check to make sure it is not Unknown or high contrast. If it is one of those, Log error.
        string baseMonacoTheme = appApplicationTheme == ApplicationTheme.Light ? "vs" : "vs-dark";

        var script = $$"""
                       monaco.editor.defineTheme('{{uiThemeName}}', {
                            base: '{{baseMonacoTheme}}',
                            inherit: true,
                            rules: [
                                { "token": "", "background": "1e1e1e00" },
                                { token: "custom-info",     foreground: "008A17" },
                                { token: "custom-error",    foreground: "ff0000", fontStyle: "bold" },
                                { token: "custom-verbose",  foreground: "029EF7" },
                                { token: "custom-warning",  foreground: "A1BA00" },
                                { token: "custom-date",     foreground: "808080" }
                            ],
                            colors: {
                                'editor.background': '#00000000',
                                'minimap.background': '#00000000'
                            }
                       });
                       monaco.editor.setTheme('{{uiThemeName}}');
                       """;
        await ExecuteScriptWithLoggingAsync(script);
    }
    
    public async Task AddCustomLanguageToEditorAsync(IProgrammingLanguage programmingLanguage) {
        await CreateCustomLanguageAsync(programmingLanguage);
        await RegisterTokensAsync(programmingLanguage);
    }
    
    private async Task CreateCustomLanguageAsync(IProgrammingLanguage programmingLanguage) {
        var script = $$"""
                       monaco.languages.register({
                           'id': '{{programmingLanguage.ToString().ToLower()}}'
                       });
                       """;
        await ExecuteScriptWithLoggingAsync(script);
    }
    
    private async Task RegisterTokensAsync(IProgrammingLanguage programmingLanguage) {
        var script = $$"""
                       monaco.languages.setMonarchTokensProvider('{{programmingLanguage.ToString().ToLower()}}', {
                           tokenizer: {
                               root: [
                                   [/\[INFO\].*\]/, "custom-info"],
                                   [/\[VERBOSE\].*\]/, "custom-verbose"],
                                   [/\[VERBOSE\].*/, "custom-verbose"],
                                   [/\[\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3}\]/, "custom-date"],
                                   [/\[ERROR\].*\]/, "custom-error"],
                                   [/\[WARNING\].*\]/, "custom-warning"]
                               ]
                           }
                       });
                       """;
            
        await ExecuteScriptWithLoggingAsync(script);
    }

    private async Task ExecuteScriptWithLoggingAsync(string script, bool verbose = true) {
        try {
            string response = await webView2.ExecuteScriptAsync(script);
            
            if (verbose) _logger.LogVerbose($"Executed script: {script} with response: {response}");
        }
        catch (Exception ex) {
            _logger.LogError($"Error executing script: {script} with message: {ex}");
        }
    }
    
    private static void RunOnUIThreadAsync(Func<Task> action) {
        Application.Current.Dispatcher.InvokeAsync(async () => await action());
    }

}
