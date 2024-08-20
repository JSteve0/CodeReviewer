using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
namespace CodeReviewer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {

    private WebViewerControl _activeWebViewerControl;

    private readonly Dictionary<string, WebViewerControl> _webViewerControls = [];
    
    public MainWindow() {
        InitializeComponent();

        _activeWebViewerControl = WebViewerControlInstance;
        _webViewerControls.Add("new.js", _activeWebViewerControl);
        
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        RunPeriodicAsync(OnTick, TimeSpan.Zero, TimeSpan.FromMilliseconds(100), CancellationToken.None);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }
    
    private static async Task RunPeriodicAsync(Action onTick,
        TimeSpan dueTime, 
        TimeSpan interval, 
        CancellationToken token) {
        // Initial wait time before we begin the periodic loop.
        if(dueTime > TimeSpan.Zero)
            await Task.Delay(dueTime, token);

        // Repeat this loop until cancelled.
        while(!token.IsCancellationRequested)
        {
            // Call our onTick function.
            onTick?.Invoke();

            // Wait to repeat again.
            if(interval > TimeSpan.Zero)
                await Task.Delay(interval, token);       
        }
    }
    
    private void OnTick() {
        _webViewerControls.ToList().ForEach(item => {
            item.Value.WebViewer.Update();
        });
    }

    private WebViewerControl AddNewTab(string tabTitle, string tabLanguage) {
        var webViewerControl = new WebViewerControl();
        TabItem tabItem = CreateNewTabItem(tabTitle, tabLanguage, webViewerControl);

        webViewerControl.UpdateLayout();
        _webViewerControls.Add(tabTitle, webViewerControl);
        Tabs.Items.Add(tabItem);

        return webViewerControl;
    }

    private TabItem CreateNewTabItem(string tabTitle, string tabLanguage, WebViewerControl webViewerControl) {

        var tabItem = new TabItem {
            Content = webViewerControl,
            Header = tabTitle
        };
        
        return tabItem;
    }

    // TODO: Pull out logic into a method
    private void JavaScriptNewFileButton_OnClick(object sender, RoutedEventArgs e) {
        var newTabName = "new.js";

        while (_webViewerControls.ContainsKey(newTabName)) {
            var splitString= newTabName.Split(".");
            newTabName = splitString[0] + " (1).js";
        }
                
        WebViewerControl webViewerControl = AddNewTab(newTabName, "javascript");
        
        webViewerControl.WebViewer.SendMessage(WebViewer.Keys.Language, "javascript");
        
        // TODO: Optimize to only update screensize on new tab
        UpdateScreenSizes();

        NewFileComboBox.SelectedIndex = -1;
    }
    
    // TODO: Pull out logic into a method
    private void CSharpNewFileButton_OnClick(object sender, RoutedEventArgs e) {
        var newTabName = "new.cs";

        while (_webViewerControls.ContainsKey(newTabName)) {
            var splitString= newTabName.Split(".");
            newTabName = splitString[0] + " (1).cs";
        }
        
        WebViewerControl webViewerControl = AddNewTab(newTabName, "csharp");
        
        // TODO: Optimize to only update screensize on new tab
        UpdateScreenSizes();
        
        webViewerControl.WebViewer.SendMessage(WebViewer.Keys.Language, "csharp");
    }
    
    private void JavaScriptButton_OnClick(object sender, RoutedEventArgs e) {
        _activeWebViewerControl.WebViewer.SendMessage(WebViewer.Keys.Language, "javascript");
    }
    
    private void CSharpButton_OnClick(object sender, RoutedEventArgs e) {
        _activeWebViewerControl.WebViewer.SendMessage(WebViewer.Keys.Language, "csharp");
    }
    
    private void ClearAllTextButton_OnClick(object sender, RoutedEventArgs e) {
        _activeWebViewerControl.WebViewer.SendMessage(WebViewer.Keys.Action, "clear");
    }
    
    private void OnSizeChanged(object sender, SizeChangedEventArgs e) {
        UpdateScreenSizes();
    }

    private void UpdateScreenSizes() {
        double[] computedWidth = [Math.Max(ActualWidth - 200, 0.0), 200.0];
        
        MainGrid.ColumnDefinitions[0].Width = new GridLength(computedWidth[0]);
        _activeWebViewerControl.Width = computedWidth[0];
        
        MainGrid.ColumnDefinitions[1].Width = new GridLength(computedWidth[1]);
        ActionsPanel.Width = computedWidth[1];

        MainGrid.RowDefinitions[0].Height = new GridLength(ActualHeight);

        foreach (var webViewerControl in _webViewerControls) {
            webViewerControl.Value.WebView.Height = ActualHeight;
        }
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        string? tabTitle = ((TabItem)e.AddedItems[0]!).Header.ToString();

        if (tabTitle != null && _webViewerControls.TryGetValue(tabTitle, out WebViewerControl? webViewerControl)) {
            _activeWebViewerControl = webViewerControl;
            Title = tabTitle;
        }
    }
}
