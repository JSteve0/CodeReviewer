using System.Reflection;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.ViewModels;
using CodeReviewerTests.UnitTests.Helper;
using Moq;
using Wpf.Ui.Appearance;
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable MemberCanBeMadeStatic.Local

namespace CodeReviewerTests.UnitTests.ViewModels;

public class EditorViewModelTests {
    
    private EditorPackage CreateViewModel() {
        return new EditorPackage();
    }

    [StaFact]
    public async Task InitializeEditorAsync_SetsInitialValues() {
        var viewModelPackage = new EditorPackage();

        // Set up mocks to return expected values
        viewModelPackage.EditorWindowController
            .Setup(m => m.CreateAsync())
            .Returns(Task.CompletedTask);

        viewModelPackage.EditorWindowController
            .Setup(m => m.SetThemeAsync(It.IsAny<ApplicationTheme>()))
            .Returns(Task.CompletedTask);

        viewModelPackage.EditorWindowController
            .Setup(m => m.SetLanguageAsync(It.IsAny<ProgrammingLanguagesEnum>()))
            .Returns(Task.CompletedTask);

        viewModelPackage.EditorWindowController
            .Setup(m => m.SetContentAsync(It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Access private method InitializeEditorAsync using reflection
        var methodInfo = typeof(EditorViewModal).GetMethod("InitializeEditorAsync", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) {
            Assert.Fail("Method InitializeEditorAsync not found.");
            return;
        }

        // Act
        await (Task)methodInfo.Invoke(viewModelPackage.EditorViewModal, null)!;

        // Assert
        var editorModelField = typeof(EditorViewModal).GetField("_editorModel", BindingFlags.NonPublic | BindingFlags.Instance);
        if (editorModelField == null) {
            Assert.Fail("Field _editorModel not found.");
            return;
        }

        var editorModel = (EditorModel)editorModelField.GetValue(viewModelPackage.EditorViewModal)!;
        Assert.Equal(ProgrammingLanguagesEnum.JavaScript, editorModel.CurrentLanguage);
        Assert.Equal(ProgrammingLanguagesEnum.JavaScript.ToString(), viewModelPackage.EditorViewModal.InfoText);

        // Verify that the methods on the mock were called as expected
        viewModelPackage.EditorWindowController.Verify(m => m.CreateAsync(), Times.Exactly(1));
        viewModelPackage.EditorWindowController.Verify(m => m.SetThemeAsync(It.IsAny<ApplicationTheme>()), Times.Once);
        viewModelPackage.EditorWindowController.Verify(m => m.SetLanguageAsync(ProgrammingLanguagesEnum.JavaScript), Times.Once);
        viewModelPackage.EditorWindowController.Verify(m => m.SetContentAsync(It.IsAny<string>()), Times.Once);
    }

    [StaFact]
    public void InitializeCommands_SetsUpCommands()
    {
        // Arrange
        var viewModel = CreateViewModel().EditorViewModal;

        // Access private method InitializeCommands using reflection
        var methodInfo = typeof(EditorViewModal).GetMethod("InitializeCommands", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) {
            Assert.Fail("Method InitializeCommands not found.");
            return;
        }

        // Act
        methodInfo.Invoke(viewModel, null);

        // Assert
        Assert.NotNull(viewModel.SaveFile);
        Assert.NotNull(viewModel.OpenFile);
        Assert.NotNull(viewModel.NewFile);
    }

    [StaFact]
    public void OnProgrammingLanguageChanged_UpdatesInfoTextForCSharp() {
        // Arrange
        var mockEditorModel = new Mock<IEditorModel>();

        // Setup the properties to return specific values
        mockEditorModel.SetupGet(m => m.CurrentLanguage).Returns(ProgrammingLanguagesEnum.CSharp);
        mockEditorModel.SetupGet(m => m.FilePath).Returns("path/to/file.cs");

        var viewModel = CreateViewModel().EditorViewModal;
        
        var editorModelField = typeof(EditorViewModal).GetField("_editorModel", BindingFlags.NonPublic | BindingFlags.Instance);
        if (editorModelField == null) {
            Assert.Fail("Field _editorModel not found.");
            return;
        }
        
        editorModelField.SetValue(viewModel, mockEditorModel.Object);
        
        var methodInfo = typeof(EditorViewModal).GetMethod("OnProgrammingLanguageChanged", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) {
            Assert.Fail("Method OnProgrammingLanguageChanged not found.");
            return;
        }
        
        // Act
        methodInfo.Invoke(viewModel, [null, EventArgs.Empty]);

        // Assert
        Assert.Equal("CSharp | path/to/file.cs", viewModel.InfoText);
    }
    
    [StaFact]
    public void OnProgrammingLanguageChanged_UpdatesInfoTextForJavaScript() {
        // Arrange
        var mockEditorModel = new Mock<IEditorModel>();

        // Setup the properties to return specific values
        mockEditorModel.SetupGet(m => m.CurrentLanguage).Returns(ProgrammingLanguagesEnum.JavaScript);
        mockEditorModel.SetupGet(m => m.FilePath).Returns("path/of/file.js");

        var viewModel = CreateViewModel().EditorViewModal;
        
        var editorModelField = typeof(EditorViewModal).GetField("_editorModel", BindingFlags.NonPublic | BindingFlags.Instance);
        if (editorModelField == null) {
            Assert.Fail("Field _editorModel not found.");
            return;
        }
        
        editorModelField.SetValue(viewModel, mockEditorModel.Object);
        
        var methodInfo = typeof(EditorViewModal).GetMethod("OnProgrammingLanguageChanged", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) {
            Assert.Fail("Method OnProgrammingLanguageChanged not found.");
            return;
        }
        
        // Act
        methodInfo.Invoke(viewModel, [null, EventArgs.Empty]);

        // Assert
        Assert.Equal("JavaScript | path/of/file.js", viewModel.InfoText);
    }

    /*[Fact]
    public void OnFileChanged_UpdatesInfoText()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var editorModel = new EditorModel((EventHandler)null, (EventHandler)null);
        viewModel.GetType().GetField("_editorModel", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(viewModel, editorModel);

        // Act
        editorModel.CurrentLanguage = ProgrammingLanguagesEnum.JavaScript;
        editorModel.FilePath = "NewTestPath";

        // Access private method OnFileChanged using reflection
        var methodInfo = typeof(EditorViewModal).GetMethod("OnFileChanged", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) throw new InvalidOperationException("Method not found");

        methodInfo.Invoke(viewModel, new object[] { null, EventArgs.Empty });

        // Assert
        Assert.Equal("JavaScript | NewTestPath", viewModel.InfoText);
    }*/
}
