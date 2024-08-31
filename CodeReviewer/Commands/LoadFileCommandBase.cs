using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;

namespace CodeReviewer.Commands;

/// <summary>
///     Base class for commands that involve loading files into the editor.
/// </summary>
public abstract class LoadFileCommandBase(IEditorWindowController editorWindowController, IEditorModel editorModel)
    : CommandBase {

    /// <summary>
    ///     Sets the programming language for the editor and updates the model.
    /// </summary>
    /// <param name="programmingLanguage">The programming language to set in the editor. Can be <c>null</c>.</param>
    private void SetLanguage(IProgrammingLanguage? programmingLanguage) {
        editorModel.CurrentLanguage = programmingLanguage;
        _ = editorWindowController.SetLanguageAsync(programmingLanguage);
    }

    /// <summary>
    ///     Creates a new editor instance with the specified programming language.
    ///     Initializes the editor content with the starting code for the language.
    /// </summary>
    /// <param name="programmingLanguage">The programming language to initialize the editor with. <c>Can be null</c>.</param>
    protected void CreateNewEditor(IProgrammingLanguage? programmingLanguage) {
        SetLanguage(programmingLanguage);
        _ = editorWindowController.SetContentAsync(programmingLanguage != null
            ? programmingLanguage.GetStartingCode()
            : "");

        editorModel.FilePath = null;

        Logger.LogInfo($"Created and opened a new {programmingLanguage!.ToString()} file");
    }

    /// <summary>
    ///     Creates a new editor instance, sets the content from a file, and updates the file path in the model.
    /// </summary>
    /// <param name="programmingLanguage">The programming language associated with the file. Can be null.</param>
    /// <param name="fileText">The text content to load into the editor.</param>
    /// <param name="fileName">The file path or name to associate with the editor model.</param>
    protected void CreateNewEditorFromFile(IProgrammingLanguage? programmingLanguage, string fileText,
        string fileName) {
        SetLanguage(programmingLanguage);
        _ = editorWindowController.SetContentAsync(fileText);

        editorModel.FilePath = fileName;

        Logger.LogInfo($"Opened a {programmingLanguage!.ToString()} file");
    }

}
