using CodeReviewer.Models;
using CodeReviewer.Models.Languages;

namespace CodeReviewerTests.UnitTests;

public class EditorModelTests {

    private readonly EditorModel _editorModel;
    private bool _filePathChanged;
    private bool _languageChanged;

    public EditorModelTests() {
        _languageChanged = false;
        _filePathChanged = false;

        _editorModel = new EditorModel(OnLanguageChanged, OnFilePathChanged);
    }

    public static IEnumerable<object?[]> EditorModelTestData => new List<object?[]> {
        // Test changing the language
        new object?[] { new CSharpProgrammingLanguage(), null, true, false }, // Change language to CSharp
        new object?[] { new JavaScriptProgrammingLanguage(), null, true, false }, // Change language to JavaScript

        // Test changing the file path
        new object?[] { null, "example.cs", false, true }, // Change file path to "example.cs"
        new object?[] { null, "path/to/file.js", false, true }, // Change file path to "path/to/file.js"

        // Test changing both language and file path
        new object[] {
            new JavaScriptProgrammingLanguage(), "example.js", true, true
        }, // Change language to JavaScript and file path to "example.js"
        new object[] {
            new CSharpProgrammingLanguage(), "path/to/file.cs", true, true
        }, // Change language to CSharp and file path to "path/to/file.cs"

        // Test setting the same language and file path again (should still trigger events)
        new object[]
            { new CSharpProgrammingLanguage(), "example.cs", true, true }, // Set the same language and file path again

        // Test resetting values
        new object?[] { null, null, false, false }, // Reset both language and file path

        // Test changing language and resetting file path
        new object?[] {
            new JavaScriptProgrammingLanguage(), null, true, false
        }, // Change language to JavaScript and reset file path

        // Test changing file path and resetting language
        new object?[] { null, "reset.js", false, true }, // Reset language and change file path

        // Test changing language twice
        new object?[] { new CSharpProgrammingLanguage(), null, true, false }, // Change language to CSharp
        new object?[] { new JavaScriptProgrammingLanguage(), null, true, false }, // Then change language to JavaScript

        // Test changing file path twice
        new object?[] { null, "first/path.js", false, true }, // Change file path to "first/path.js"
        new object?[] { null, "second/path.cs", false, true } // Then change file path to "second/path.cs"
    };

    // Event handlers to simulate event handling
    private void OnLanguageChanged(object? sender, EventArgs e) {
        _languageChanged = true;
    }

    private void OnFilePathChanged(object? sender, EventArgs e) {
        _filePathChanged = true;
    }

    [Theory]
    [MemberData(nameof(EditorModelTestData))]
    public void EditorModel_SetProperties_TriggersCorrectEvents(
        IProgrammingLanguage? newLanguage,
        string? newFilePath,
        bool expectedLanguageChanged,
        bool expectedFilePathChanged) {
        // Act
        if (newLanguage != null)
            _editorModel.CurrentLanguage = newLanguage;

        if (newFilePath != null)
            _editorModel.FilePath = newFilePath;

        // Assert
        Assert.Equal(expectedLanguageChanged, _languageChanged);
        Assert.Equal(expectedFilePathChanged, _filePathChanged);

        if (newLanguage != null)
            Assert.Equal(newLanguage, _editorModel.CurrentLanguage);

        if (newFilePath != null)
            Assert.Equal(newFilePath, _editorModel.FilePath);
    }

}
