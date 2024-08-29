using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;

namespace CodeReviewer.Commands;

public abstract class LoadFileCommandBase(IEditorWindowController editorWindowController, IEditorModel editorModel) : CommandBase {
    private void SetLanguage(IProgrammingLanguage? programmingLanguage) {
        editorModel.CurrentLanguage = programmingLanguage;
        _ = editorWindowController.SetLanguageAsync(programmingLanguage);
    }

    protected void CreateNewEditor(IProgrammingLanguage? programmingLanguage) {
        SetLanguage(programmingLanguage);
        _ = editorWindowController.SetContentAsync(programmingLanguage != null 
            ? programmingLanguage.GetStartingCode()
            : "");

        editorModel.FilePath = null;
    }

    protected void CreateNewEditorFromFile(IProgrammingLanguage? programmingLanguage, string fileText, string fileName) {
        SetLanguage(programmingLanguage);
        _ = editorWindowController.SetContentAsync(fileText);

        editorModel.FilePath = fileName;
    }
    
}
