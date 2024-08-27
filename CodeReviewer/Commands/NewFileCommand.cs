using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;

namespace CodeReviewer.Commands;

public class NewFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel) : LoadFileCommandBase(editorWindowController, editorModel) {

    public override void Execute(object? parameter) => 
        CreateNewEditor(
            parameter != null 
            && Enum.TryParse(parameter.ToString(), out ProgrammingLanguagesEnum newProgrammingLanguage) 
                ? newProgrammingLanguage 
                : null);
    
}
