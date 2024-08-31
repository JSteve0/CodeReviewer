using CodeReviewer.Controllers;

namespace CodeReviewer.Windows;

public partial class MainWindow {

    public MainWindow() {
        InitializeComponent();

        var editorViewModal = new EditorViewModal(WebView, new EditorWindowController(WebView));

        DataContext = editorViewModal;
    }

}
