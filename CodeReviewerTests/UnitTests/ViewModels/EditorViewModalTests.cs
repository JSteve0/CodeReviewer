using System.Reflection;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;
using CodeReviewerTests.UnitTests.Helper;
using Moq;
using Wpf.Ui.Appearance;

// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable MemberCanBeMadeStatic.Local

namespace CodeReviewerTests.UnitTests.ViewModels;

public class EditorViewModelTests {

    public static IEnumerable<object[]> ProgrammingLanguageTestData => new List<object[]> {
        new object[] { ProgrammingLanguages.Languages.Find(pl => pl.Extension == "cs")!, "path/of/the/file.cs" },
        new object[] {
            ProgrammingLanguages.Languages.Find(pl => pl.Extension == "java")!, "really/long/file/path/for/testing.java"
        },
        new object[] { ProgrammingLanguages.Languages.Find(pl => pl.Extension == "js")!, "path/of/file.js" },
        new object[] { ProgrammingLanguages.Languages.Find(pl => pl.Extension == "ts")!, "path/file.ts" }
    };

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
                        .Setup(m => m.SetLanguageAsync(It.IsAny<IProgrammingLanguage>()))
                        .Returns(Task.CompletedTask);

        viewModelPackage.EditorWindowController
                        .Setup(m => m.SetContentAsync(It.IsAny<string>()))
                        .Returns(Task.CompletedTask);

        // Access private method InitializeEditorAsync using reflection
        var methodInfo =
            typeof(EditorViewModal).GetMethod("InitializeEditorAsync", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) {
            Assert.Fail("Method InitializeEditorAsync not found.");
            return;
        }

        // Act
        methodInfo.Invoke(viewModelPackage.EditorViewModal, [this, EventArgs.Empty]);

        // Assert
        var editorModelField =
            typeof(EditorViewModal).GetField("_editorModel", BindingFlags.NonPublic | BindingFlags.Instance);
        if (editorModelField == null) {
            Assert.Fail("Field _editorModel not found.");
            return;
        }

        IProgrammingLanguage cSharp = ProgrammingLanguages.Languages.Find(pl => pl.Extension == "cs")!;

        var editorModel = (EditorModel)editorModelField.GetValue(viewModelPackage.EditorViewModal)!;
        Assert.Equal(cSharp, editorModel.CurrentLanguage);
        Assert.Contains(cSharp.ToString(), viewModelPackage.EditorViewModal.InfoText);

        // Verify that the methods on the mock were called as expected
        viewModelPackage.EditorWindowController.Verify(m => m.CreateAsync(), Times.Exactly(1));
        viewModelPackage.EditorWindowController.Verify(m => m.SetThemeAsync(It.IsAny<ApplicationTheme>()), Times.Once);
        viewModelPackage.EditorWindowController.Verify(m => m.SetLanguageAsync(cSharp), Times.Once);
        viewModelPackage.EditorWindowController.Verify(m => m.SetContentAsync(It.IsAny<string>()), Times.Once);
    }

    [StaFact]
    public void InitializeCommands_SetsUpCommands() {
        // Arrange
        var viewModel = CreateViewModel().EditorViewModal;

        // Access private method InitializeCommands using reflection
        var methodInfo =
            typeof(EditorViewModal).GetMethod("InitializeCommands", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) {
            Assert.Fail("Method InitializeCommands not found.");
            return;
        }

        // Act
        methodInfo.Invoke(viewModel, null);

        // Assert
        Assert.NotNull(viewModel.SaveFile);
        Assert.True(viewModel.SaveFile.CanExecute(null));
        
        Assert.NotNull(viewModel.OpenFile);
        Assert.True(viewModel.OpenFile.CanExecute(null));
        
        Assert.NotNull(viewModel.NewFile);
        Assert.True(viewModel.NewFile.CanExecute(null));

        Assert.NotNull(viewModel.OpenNewWindow);
        Assert.True(viewModel.OpenNewWindow.CanExecute(null));

        Assert.NotNull(viewModel.Exit);
        Assert.True(viewModel.Exit.CanExecute(null));

    }

    [StaTheory]
    [MemberData(nameof(ProgrammingLanguageTestData))]
    public void OnProgrammingLanguageChanged_UpdatesInfoText(IProgrammingLanguage programmingLanguage,
        string filePath) {
        // Arrange
        var mockEditorModel = new Mock<IEditorModel>();

        // Set up the properties to return specific values
        mockEditorModel.SetupGet(m => m.CurrentLanguage).Returns(programmingLanguage);
        mockEditorModel.SetupGet(m => m.FilePath).Returns(filePath);
        mockEditorModel
            .Setup(m => m.ToString())
            .Returns($"{mockEditorModel.Object.CurrentLanguage} | {mockEditorModel.Object.FilePath}");

        var viewModel = CreateViewModel().EditorViewModal;

        var editorModelField =
            typeof(EditorViewModal).GetField("_editorModel", BindingFlags.NonPublic | BindingFlags.Instance);
        if (editorModelField == null) {
            Assert.Fail("Field _editorModel not found.");
            return;
        }

        editorModelField.SetValue(viewModel, mockEditorModel.Object);

        var methodInfo = typeof(EditorViewModal).GetMethod("OnProgrammingLanguageChanged",
            BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) {
            Assert.Fail("Method OnProgrammingLanguageChanged not found.");
            return;
        }

        // Act
        methodInfo.Invoke(viewModel, [null, EventArgs.Empty]);

        // Assert
        Assert.Contains(programmingLanguage.ToString(), viewModel.InfoText);
        Assert.Contains(filePath, viewModel.InfoText);
    }

    [StaTheory]
    [MemberData(nameof(ProgrammingLanguageTestData))]
    public void OnFileChanged_UpdatesInfoText(IProgrammingLanguage programmingLanguage,
        string filePath) {
        // Arrange
        var mockEditorModel = new Mock<IEditorModel>();

        // Set up the properties to return specific values
        mockEditorModel.SetupGet(m => m.CurrentLanguage).Returns(programmingLanguage);
        mockEditorModel.SetupGet(m => m.FilePath).Returns(filePath);
        mockEditorModel
            .Setup(m => m.ToString())
            .Returns($"{mockEditorModel.Object.CurrentLanguage} | {mockEditorModel.Object.FilePath}");

        var viewModel = CreateViewModel().EditorViewModal;

        var editorModelField =
            typeof(EditorViewModal).GetField("_editorModel", BindingFlags.NonPublic | BindingFlags.Instance);
        if (editorModelField == null) {
            Assert.Fail("Field _editorModel not found.");
            return;
        }

        editorModelField.SetValue(viewModel, mockEditorModel.Object);

        var methodInfo = typeof(EditorViewModal).GetMethod("OnFileChanged",
            BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo == null) {
            Assert.Fail("Method OnFileChanged not found.");
            return;
        }

        // Act
        methodInfo.Invoke(viewModel, [null, EventArgs.Empty]);

        // Assert
        Assert.Contains(programmingLanguage.ToString(), viewModel.InfoText);
        Assert.Contains(filePath, viewModel.InfoText);
    }

}
