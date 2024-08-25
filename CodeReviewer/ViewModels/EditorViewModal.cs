using System.Windows;
using System.Windows.Threading;
using CodeReviewer.Commands;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Wpf.Ui.Appearance;

namespace CodeReviewer.ViewModels;

public class EditorViewModal : ViewModelBase {
    private readonly EditorWindowController _editorWindowController;
    private readonly EditorModel _editorModel = new EditorModel();

    private string _infoText = "";
    
    public string InfoText {
        get => _infoText;
        set {
            _infoText = value;
            OnPropertyChanged(nameof(InfoText));
        }
    }

    public SaveFileCommand SaveFile { get; }

    public EditorViewModal(WebView2 webView) {
        webView.NavigationCompleted += OnWebViewNavigationCompleted;
        webView.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, true);
        webView.SetCurrentValue(WebView2.DefaultBackgroundColorProperty, System.Drawing.Color.Transparent);
        webView.SetCurrentValue(
            WebView2.SourceProperty,
            new Uri(
                System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "MonacoEditor/index.html"
                )
            )
        );

        _editorWindowController = new EditorWindowController(webView);

        SaveFile = new SaveFileCommand(_editorWindowController);
    }
    
    private async Task InitializeEditorAsync() {
        const ProgrammingLanguagesEnum startingLanguage = ProgrammingLanguagesEnum.JavaScript;
        
        await _editorWindowController.CreateAsync();
        await _editorWindowController.SetThemeAsync(ApplicationThemeManager.GetAppTheme());
        await _editorWindowController.SetLanguageAsync(startingLanguage);
        await _editorWindowController.SetContentAsync(ProgrammingLanguages.GetStartingCode(startingLanguage));

        _editorModel.CurrentLanguage = startingLanguage;
        InfoText = ProgrammingLanguages.GetExtension(startingLanguage);
    }

    private void OnWebViewNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        DispatchAsync(InitializeEditorAsync);
    }
    
    private static DispatcherOperation<TResult> DispatchAsync<TResult>(Func<TResult> callback)
    {
        return Application.Current.Dispatcher.InvokeAsync(callback);
    }
}
