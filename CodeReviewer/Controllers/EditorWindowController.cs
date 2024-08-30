using System.Windows;
using CodeReviewer.Models.Languages;
using Microsoft.Web.WebView2.Wpf;
using Wpf.Ui.Appearance;

namespace CodeReviewer.Controllers;

public class EditorWindowController(WebView2 webView2) : IEditorWindowController {
    private const string EditorContainerSelector = "#root";
    private const string EditorObject = "wpfUiMonacoEditor";

    private async Task ExecuteScriptWithLoggingAsync(string script) {
        try {
            await webView2.ExecuteScriptAsync(script);
        } catch (Exception ex) {
            Console.WriteLine($"Error executing script: {ex.Message}");
        }
    }

    public async Task CreateAsync() {
        const string script = $$"""
                                const {{EditorObject}} = monaco.editor.create(document.querySelector('{{EditorContainerSelector}}'));
                                window.onresize = () => {{{EditorObject}}.layout()};
                                """;
        await ExecuteScriptWithLoggingAsync(script);
    }

    public async Task SetThemeAsync(ApplicationTheme appApplicationTheme) {
        const string uiThemeName = "wpf-ui-app-theme";
        string baseMonacoTheme = appApplicationTheme == ApplicationTheme.Light ? "vs" : "vs-dark";

        var script = $$$"""
                        monaco.editor.defineTheme('{{{uiThemeName}}}', {
                            base: '{{{baseMonacoTheme}}}',
                            inherit: true,
                            rules: [{ background: 'FFFFFF00' }],
                            colors: {'editor.background': '#FFFFFF00','minimap.background': '#FFFFFF00',}});
                        monaco.editor.setTheme('{{{uiThemeName}}}');
                        """;
        await ExecuteScriptWithLoggingAsync(script);
    }

    public async Task SetLanguageAsync(IProgrammingLanguage? programmingLanguage) {
        string languageId = programmingLanguage?.ToString().ToLower() ?? "plaintext";

        string script = "monaco.editor.setModelLanguage(" + EditorObject + $".getModel(), \"{languageId}\");";
        await ExecuteScriptWithLoggingAsync(script);
    }

    public async Task SetContentAsync(string contents) {
        string literalContents = Utils.Utils.EncodeJsString(contents);

        string script = EditorObject + $".setValue({literalContents});";
        await ExecuteScriptWithLoggingAsync(script);
    }

    public async Task<string> GetContent() {
        var result = string.Empty;
        try {
            result = await webView2.ExecuteScriptAsync(EditorObject + ".getValue()");
        } catch (Exception ex) {
            Console.WriteLine($"Error in GetContent: {ex.Message}");
        }
        return result;
    }

    public void DispatchScript(string script) {
        Application.Current.Dispatcher.InvokeAsync(async () => await ExecuteScriptWithLoggingAsync(script));
    }
}
