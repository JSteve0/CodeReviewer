using CodeReviewer.Controllers;
using CodeReviewer.Models;

namespace CodeReviewer.Commands;

public class NewLoadFileCommandBase(IEditorWindowController editorWindowController, IEditorModel editorModel) : LoadFileCommandBase(editorWindowController, editorModel) {

    public override void Execute(object? parameter) => 
        CreateNewEditor(
            parameter != null 
            && Enum.TryParse(parameter.ToString(), out ProgrammingLanguagesEnum newProgrammingLanguage) 
                ? newProgrammingLanguage 
                : null);
    
}
