using CodeReviewer.Controllers;
using CodeReviewer.ViewModels;
using Wpf.Ui.Controls;

namespace CodeReviewer.Windows;

public partial class MainWindow : FluentWindow {
    public MainWindow()
    {
        InitializeComponent();

        var editorViewModal = new EditorViewModal(WebView, new EditorWindowController(WebView));

        DataContext = editorViewModal;
    }
    
    
}
