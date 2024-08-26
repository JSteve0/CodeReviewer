using CodeReviewer.Controllers;
using CodeReviewer.Models;

namespace CodeReviewer.Commands;

public abstract class LoadFileCommandBase(IEditorWindowController editorWindowController, IEditorModel editorModel) : CommandBase{
    
    public void SetLanguage(ProgrammingLanguagesEnum? programmingLanguage) {
        editorModel.CurrentLanguage = programmingLanguage;
        _ = editorWindowController.SetLanguageAsync(programmingLanguage);
    }

    public void CreateNewEditor(ProgrammingLanguagesEnum? programmingLanguage) {
        SetLanguage(programmingLanguage);
        _ = editorWindowController.SetContentAsync(programmingLanguage != null 
            ? ProgrammingLanguages.GetStartingCode((ProgrammingLanguagesEnum)programmingLanguage)
            : "");

        editorModel.FilePath = null;
    }
    
    public void CreateNewEditorFromFile(ProgrammingLanguagesEnum? programmingLanguage, string fileText, string fileName) {
        SetLanguage(programmingLanguage);
        _ = editorWindowController.SetContentAsync(fileText);

        editorModel.FilePath = fileName;
    }
    
}
