using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Wpf;

namespace CodeReviewer;

public partial class WebViewerControl : UserControl {
    
    public WebViewer WebViewer { private set; get; }
    
    public WebViewerControl() {
        InitializeComponent();
            
        DataContext = this;
        
        WebViewer = new WebViewer("http://127.0.0.1:5500/index.html", WebView);
    }
}
