using System.Drawing;
using System.IO;
using System.Windows;
using CodeReviewer.Logging;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace CodeReviewer.Services;

public class WebViewInitializer {
    private readonly EventHandler _onInitializedEventHandler;
    private readonly WebView2 _webView;

    public WebViewInitializer(WebView2 webView, EventHandler onInitializedEventHandler) {
        _webView = webView;
        _onInitializedEventHandler += onInitializedEventHandler;
        Initialize();
    }

    private void Initialize() {
        ConsoleLogger.Instance.LogInfo("Initializing Web View");

        _webView.NavigationCompleted += OnWebViewNavigationCompleted;
        _webView.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, true);
        _webView.SetCurrentValue(WebView2.DefaultBackgroundColorProperty, Color.Transparent);
        _webView.SetCurrentValue(
            WebView2.SourceProperty,
            new Uri(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "MonacoEditor/index.html"
                )
            )
        );
    }

    private void OnWebViewNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e) {
        ConsoleLogger.Instance.LogInfo("Finished initialization of Web View");
        Application.Current.Dispatcher.InvokeAsync(() => _onInitializedEventHandler.Invoke(this, EventArgs.Empty));
    }
}