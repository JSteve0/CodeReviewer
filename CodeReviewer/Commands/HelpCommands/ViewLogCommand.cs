using System.IO;
using System.Windows.Input;
using CodeReviewer.Commands.FileCommands;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;

namespace CodeReviewer.Commands.HelpCommands;

public class ViewLogCommand(IEditorWindowController editorWindowController, IEditorModel editorModel) : LoadFileCommandBase(editorWindowController, editorModel) {

    public override Key GestureKey { get; protected set; } = Key.L;
    public override string GestureKeyText { get; protected set; } = "Ctrl+Alt+L";
    public override ModifierKeys GestureModifier { get; protected set; } = ModifierKeys.Control | ModifierKeys.Alt;
    
    public override void Execute(object? parameter) {
        try {
            string? logFile = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.crlog").FirstOrDefault();

            if (logFile == null) {
                Logger.LogWarning("No log file found");
                return;
            }

            CreateNewEditorFromFile(
                ProgrammingLanguages.GetProgrammingLanguageFromExtension(".crlog")
                , File.ReadAllText(logFile)
                , logFile);
        }
        catch (Exception ex) {
            Logger.LogError($"Error loading log file: {ex}");
        }
    }

}
