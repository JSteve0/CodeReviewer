using CodeReviewer.Controllers;
using CodeReviewer.ViewModels;

namespace CodeReviewer.Views;

public partial class MainWindow {

    public MainWindow() {
        InitializeComponent();

        var editorViewModal = new EditorViewModal(WebView, new EditorWindowController(WebView), this);

        DataContext = editorViewModal;
    }

}
