using System.Windows;
using CodeReviewer.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Web.WebView2.Wpf;
using Wpf.Ui.Appearance;

namespace CodeReviewer.Controllers;

public class EditorWindowController(WebView2 webView2) : IEditorWindowController {
     private const string EditorContainerSelector = "#root";

    private const string EditorObject = "wpfUiMonacoEditor";

    public async Task CreateAsync()
    {
        _ = await webView2.ExecuteScriptAsync(
            $$"""
            const {{EditorObject}} = monaco.editor.create(document.querySelector('{{EditorContainerSelector}}'));
            window.onresize = () => {{{EditorObject}}.layout();}
            """
        );
    }

    public async Task SetThemeAsync(ApplicationTheme appApplicationTheme)
    {
        const string uiThemeName = "wpf-ui-app-theme";
        string baseMonacoTheme = appApplicationTheme == ApplicationTheme.Light ? "vs" : "vs-dark";
        
        Console.WriteLine(baseMonacoTheme);

        _ = await webView2.ExecuteScriptAsync(
            $$$"""
            monaco.editor.defineTheme('{{{uiThemeName}}}', {
                base: '{{{baseMonacoTheme}}}',
                inherit: true,
                rules: [{ background: 'FFFFFF00' }],
                colors: {'editor.background': '#FFFFFF00','minimap.background': '#FFFFFF00',}});
            monaco.editor.setTheme('{{{uiThemeName}}}');
            """
        );
    }

    public async Task SetLanguageAsync(ProgrammingLanguagesEnum? programmingLanguage)
    {
        string languageId = programmingLanguage != null ? programmingLanguage.ToString()!.ToLower() : "";

        await webView2.ExecuteScriptAsync(
            "monaco.editor.setModelLanguage(" + EditorObject + $".getModel(), \"{languageId}\");"
        );
    }

    public async Task SetContentAsync(string contents) {
        var literalContents = Utils.Utils.EncodeJsString(contents);
        Console.WriteLine(literalContents);

        await webView2.ExecuteScriptAsync(EditorObject + $".setValue({literalContents});");
    }

    public async Task<string> GetContent() {
        return await webView2.ExecuteScriptAsync(EditorObject + $".getValue()");
    }

    public void DispatchScript(string script) {
        Application.Current.Dispatcher.InvokeAsync(async () => await webView2!.ExecuteScriptAsync(script));
    }
}
