using CodeReviewer.Models;

namespace CodeReviewerTests.UnitTests;

public class EditorModelTests {
    
    private readonly EditorModel _editorModel;
    private bool _languageChanged;
    private bool _filePathChanged;

    public EditorModelTests()
    {
        _languageChanged = false;
        _filePathChanged = false;

        _editorModel = new EditorModel(OnLanguageChanged, OnFilePathChanged);
    }

    // Event handlers to simulate event handling
    private void OnLanguageChanged(object? sender, EventArgs e)
    {
        _languageChanged = true;
    }

    private void OnFilePathChanged(object? sender, EventArgs e)
    {
        _filePathChanged = true;
    }

    [Fact]
    public void CurrentLanguage_SetLanguage_TriggersLanguageChangedEvent()
    {
        _editorModel.CurrentLanguage = ProgrammingLanguagesEnum.CSharp;

        Assert.True(_languageChanged);
        Assert.Equal(ProgrammingLanguagesEnum.CSharp, _editorModel.CurrentLanguage);
    }

    [Fact]
    public void FilePath_SetFilePath_TriggersFilePathChangedEvent() {
        const string fileName = "example.cs";
        _editorModel.FilePath = fileName;

        Assert.True(_filePathChanged);
        Assert.Equal(fileName, _editorModel.FilePath);
    }
    
}
