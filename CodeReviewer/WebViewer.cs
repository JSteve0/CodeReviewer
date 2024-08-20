using System.Diagnostics;
using Microsoft.Web.WebView2.Wpf;

namespace CodeReviewer;

public class WebViewer {

    private readonly WebView2 _webView;

    private readonly Queue<Tuple<Keys, string>> _messageQueue = new ();
    private DateTime? _delayUntil = null;
    
    public WebViewer(string url, WebView2? webView2 = null) {
        _webView = webView2 ?? new WebView2();
        _webView.Source = new Uri(url);
    }

    public void Update() {
        if (_messageQueue.Count <= 0 || _webView.CoreWebView2 == null) return;

        _delayUntil ??= DateTime.Now.Add(TimeSpan.FromMilliseconds(200)); 
        
        if (DateTime.Now < _delayUntil) return;
        
        var message = _messageQueue.Dequeue();
        SendMessage(message.Item1.ToString(), message.Item2);
    }
    
    private void SendMessage(string key, string value) {
        _webView.CoreWebView2?.PostWebMessageAsJson($"{{\"{key.ToLower()}\": \"{value.ToLower()}\"}}");
    }
    
    public void SendMessage(Keys key, string value) {
        _messageQueue.Enqueue(new Tuple<Keys, string>(key, value));
    }
    
    public enum Keys {
        Language,
        Action
    }

}
