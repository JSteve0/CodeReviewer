using System.Windows;
using System.Windows.Threading;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CodeReviewer.Windows;

public partial class MainWindow : FluentWindow {

    private readonly EditorWindowController _editorWindowController;

    public MainWindow()
    {
        InitializeComponent();
        
        WebView.NavigationCompleted += OnWebViewNavigationCompleted;
        WebView.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, true);
        WebView.SetCurrentValue(WebView2.DefaultBackgroundColorProperty, System.Drawing.Color.Transparent);
        WebView.SetCurrentValue(
            WebView2.SourceProperty,
            new Uri(
                System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "MonacoEditor/index.html"
                )
            )
        );

        _editorWindowController = new EditorWindowController(WebView);
    }
    
    private async Task InitializeEditorAsync()
    {
        await _editorWindowController.CreateAsync();
        await _editorWindowController.SetThemeAsync(ApplicationThemeManager.GetAppTheme());
        await _editorWindowController.SetLanguageAsync(ProgrammingLanguage.JavaScript);
        await _editorWindowController.SetContentAsync("function helloWorld() {\n\tconsole.log('Hello World')\n}\n");
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
