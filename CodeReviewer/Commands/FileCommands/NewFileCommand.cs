using System.Windows.Input;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;

namespace CodeReviewer.Commands.FileCommands;

/// <summary>
///     Command to create a new file in the editor with the specified programming language.
/// </summary>
public class NewFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel)
    : LoadFileCommandBase(editorWindowController, editorModel) {

    public override Key GestureKey { get; protected set; } = Key.N;
    public override string GestureKeyText { get; protected set; } = "Ctrl+N";
    public override ModifierKeys GestureModifier { get; protected set; } = ModifierKeys.Control;

    /// <summary>
    ///     Executes the command to create a new editor instance with the specified programming language.
    ///     Retrieves the programming language from the command parameter and initializes the editor.
    /// </summary>
    /// <param name="parameter">
    ///     A string representing the name of the programming language to use in the new editor.
    ///     This parameter is optional and can be <c>null</c>.
    /// </param>
    public override void Execute(object? parameter) {
        string languageName = parameter?.ToString() ?? "CSharp";
        IProgrammingLanguage? language =
            ProgrammingLanguages.Languages.FirstOrDefault(lang => lang.ToString() == languageName);

        CreateNewEditor(language);
        
        Logger.LogInfo($"Created a new {languageName} file");
    }

}
