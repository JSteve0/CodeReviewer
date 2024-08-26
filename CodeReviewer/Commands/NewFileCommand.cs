using CodeReviewer.Controllers;
using CodeReviewer.Models;

namespace CodeReviewer.Commands;

public class NewFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel) : CommandBase {
    
    public override void Execute(object? parameter) {
        if (parameter != null && Enum.TryParse(parameter.ToString(), out ProgrammingLanguagesEnum newProgrammingLanguage)) {
            editorModel.CurrentLanguage = newProgrammingLanguage;
            editorModel.FilePath = null;

            _ = editorWindowController.SetLanguageAsync(newProgrammingLanguage);
            _ = editorWindowController.SetContentAsync(ProgrammingLanguages.GetStartingCode(newProgrammingLanguage));
        }
        else {
            editorModel.CurrentLanguage = null;
            editorModel.FilePath = null;

            _ = editorWindowController.SetLanguageAsync(null);
            _ = editorWindowController.SetContentAsync("");
        }
    }
    
}
