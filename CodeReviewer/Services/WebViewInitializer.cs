using System.Drawing;
using System.IO;
using System.Windows;
using CodeReviewer.Logging;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace CodeReviewer.Services;

/// <summary>
///     Initializes the WebView control with the necessary configuration and event handlers.
/// </summary>
public class WebViewInitializer {

    private readonly EventHandler _onInitializedEventHandler;
    private readonly WebView2 _webView;

    public WebViewInitializer(WebView2 webView, EventHandler onInitializedEventHandler) {
        _webView = webView;
        _onInitializedEventHandler = onInitializedEventHandler;
        Initialize();
    }


    private void Initialize() {
        Logger.Instance.LogInfo("Initializing Web View");

        _webView.NavigationCompleted += OnWebViewNavigationCompleted;
        _webView.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, true);
        _webView.SetCurrentValue(WebView2.DefaultBackgroundColorProperty, Color.Transparent);
        try {
            var sourceUri = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MonacoEditor/index.html"));
            _webView.SetCurrentValue(WebView2.SourceProperty, sourceUri);
        }
        catch (Exception ex) {
            Logger.Instance.LogError("Error initializing WebView source: " + ex.Message);
        }
    }

    private void OnWebViewNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e) {
        if (!e.IsSuccess) {
            Logger.Instance.LogError($"Navigation failed with error code {e.WebErrorStatus}");
            return;
        }

        Logger.Instance.LogInfo("Finished initialization of Web View");
        Application.Current.Dispatcher.InvokeAsync(() => _onInitializedEventHandler.Invoke(this, EventArgs.Empty));
    }

}
