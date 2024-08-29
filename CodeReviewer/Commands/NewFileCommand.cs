using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;

namespace CodeReviewer.Commands;

public class NewFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel)
    : LoadFileCommandBase(editorWindowController, editorModel) {
    public override void Execute(object? parameter) {
        var languageName = parameter?.ToString();
        IProgrammingLanguage? language = ProgrammingLanguages.Languages
            .FirstOrDefault(lang => lang.ToString() == languageName);

        CreateNewEditor(language);
    }
}