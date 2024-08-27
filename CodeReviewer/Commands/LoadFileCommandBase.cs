using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;

namespace CodeReviewer.Commands;

public abstract class LoadFileCommandBase(IEditorWindowController editorWindowController, IEditorModel editorModel) : CommandBase {
    private void SetLanguage(ProgrammingLanguagesEnum? programmingLanguage) {
        editorModel.CurrentLanguage = programmingLanguage;
        _ = editorWindowController.SetLanguageAsync(programmingLanguage);
    }

    protected void CreateNewEditor(ProgrammingLanguagesEnum? programmingLanguage) {
        SetLanguage(programmingLanguage);
        _ = editorWindowController.SetContentAsync(programmingLanguage != null 
            ? ProgrammingLanguages.GetStartingCode((ProgrammingLanguagesEnum)programmingLanguage)
            : "");

        editorModel.FilePath = null;
    }

    protected void CreateNewEditorFromFile(ProgrammingLanguagesEnum? programmingLanguage, string fileText, string fileName) {
        SetLanguage(programmingLanguage);
        _ = editorWindowController.SetContentAsync(fileText);

        editorModel.FilePath = fileName;
    }
    
}
