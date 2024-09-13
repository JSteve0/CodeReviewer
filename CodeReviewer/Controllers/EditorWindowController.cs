using System.Windows;
using CodeReviewer.Models.Languages;
using Microsoft.Web.WebView2.Wpf;
using Wpf.Ui.Appearance;

namespace CodeReviewer.Controllers;

public class EditorWindowController(WebView2 webView2) : IEditorWindowController {

    private const string EditorContainerSelector = "#root";
    private const string EditorObject = "wpfUiMonacoEditor";

    /// <summary>
    ///     Creates a new async task to initialize the Monaco Editor.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CreateAsync() {
        const string script = $$"""
                                const {{EditorObject}} = monaco.editor.create(document.querySelector('{{EditorContainerSelector}}'));
                                window.onresize = () => {{{EditorObject}}.layout()};
                                """;
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
                            rules: [{
                                "background": "1e1e1e00",
                                "token": ""
                            }],
                            colors: {
                                'editor.background': '#00000000',
                                'minimap.background': '#00000000'
                            }
                        });
                        monaco.editor.setTheme('{{uiThemeName}}');
                        """;
        await ExecuteScriptWithLoggingAsync(script);
    }

    /// <summary>
    ///     Sets the language of the Monaco Editor asynchronously.
    /// </summary>
    /// <param name="programmingLanguage">The programming language to set. If null, the language will be set to "plaintext".</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SetLanguageAsync(IProgrammingLanguage? programmingLanguage) {
        string languageId = programmingLanguage?.ToString().ToLower() ?? "plaintext";

        string script = "monaco.editor.setModelLanguage(" + EditorObject + $".getModel(), \"{languageId}\");";
        await ExecuteScriptWithLoggingAsync(script);
    }

    /// <summary>
    ///     Sets the content of the Monaco Editor.
    /// </summary>
    /// <param name="contents">The content to be set in the editor.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SetContentAsync(string contents) {
        string literalContents = Utils.Utils.EncodeJsString(contents);

        string script = EditorObject + $".setValue({literalContents});";
        await ExecuteScriptWithLoggingAsync(script);
    }

    /// <summary>
    ///     Retrieves the content of the Monaco Editor asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result represents the content of the Monaco Editor.</returns>
    public async Task<string> GetContent() {
        var result = string.Empty;
        try {
            result = await webView2.ExecuteScriptAsync(EditorObject + ".getValue()");
        }
        catch (Exception ex) {
            Console.WriteLine($"Error in GetContent: {ex.Message}");
        }

        return result;
    }

    /// <summary>
    ///     Dispatches a script to the UI thread for execution.
    /// </summary>
    /// <param name="script">The script to be executed.</param>
    public void DispatchScript(string script) {
        Application.Current.Dispatcher.InvokeAsync(async () => await ExecuteScriptWithLoggingAsync(script));
    }

    private async Task ExecuteScriptWithLoggingAsync(string script) {
        try {
            await webView2.ExecuteScriptAsync(script);
        }
        catch (Exception ex) {
            Console.WriteLine($"Error executing script: {ex.Message}");
        }
    }

}
