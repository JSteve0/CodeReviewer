using CodeReviewer.Controllers;
using CodeReviewer.ViewModels;
using Microsoft.Web.WebView2.Wpf;
using Moq;

namespace CodeReviewerTests.UnitTests.Helper;

public class EditorPackage {

    public EditorPackage() {
        WebViewMock = new Mock<WebView2>();
        EditorWindowController = new Mock<IEditorWindowController>();
        EditorViewModal = new EditorViewModal(WebViewMock.Object, EditorWindowController.Object);
    }

    public Mock<WebView2> WebViewMock { get; set; }

    public Mock<IEditorWindowController> EditorWindowController { get; set; }

    public EditorViewModal EditorViewModal { get; set; }

}
