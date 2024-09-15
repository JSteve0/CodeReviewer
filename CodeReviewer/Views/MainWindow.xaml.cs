using System.Diagnostics;
using CodeReviewer.Controllers;
using CodeReviewer.ViewModels;

namespace CodeReviewer.Windows;

public partial class MainWindow {

    public MainWindow() {
        InitializeComponent();

        var editorViewModal = new EditorViewModal(WebView, new EditorWindowController(WebView), this);

        DataContext = editorViewModal;
    }

}
