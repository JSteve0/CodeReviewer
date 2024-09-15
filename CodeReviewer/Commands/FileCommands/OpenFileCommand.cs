using System.IO;
using System.Text;
using System.Windows.Input;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;
using Microsoft.Win32;

namespace CodeReviewer.Commands.FileCommands;

/// <summary>
///     Command to open a file and load its content into the editor.
/// </summary>
public class OpenFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel)
    : LoadFileCommandBase(editorWindowController, editorModel) {

    public override Key GestureKey { get; protected set; } = Key.O;
    public override string GestureKeyText { get; protected set; } = "Crtl+O";
    public override ModifierKeys GestureModifier { get; protected set; } = ModifierKeys.Control;

    /// <summary>
    ///     Escapes special characters in the text for use in JavaScript.
    /// </summary>
    /// <param name="text">The text to escape.</param>
    /// <returns>The escaped text.</returns>
    private static string EscapeJavaScriptString(string text) {
        return text.Replace("\"", "\\\"");
    }

    /// <summary>
    ///     Executes the command to open a file dialog, read the selected file's content, and initialize the editor with the
    ///     file's content.
    /// </summary>
    /// <param name="parameter">
    ///     Optional parameter for the command. Not used in this implementation and can be <c>null</c>.
    /// </param>
    public override void Execute(object? parameter) {
        var openFileDialog = new OpenFileDialog {
            Filter = "All Files (*.*)|*.*" // Set a filter for file types if needed
        };

        bool? isFileSelected = openFileDialog.ShowDialog();

        if (isFileSelected != true) {
            Logger.LogWarning("No file selected");
            return;
        }

        string filePath = openFileDialog.FileName;

        try {
            string fileText = File.ReadAllText(filePath, Encoding.UTF8);
            string escapedFileText = EscapeJavaScriptString(fileText);
            string fileExtension = Path.GetExtension(filePath)[1..];

            IProgrammingLanguage? newLanguage =
                ProgrammingLanguages.GetProgrammingLanguageFromExtension(fileExtension);

            CreateNewEditorFromFile(newLanguage, escapedFileText, filePath);
            
            Logger.LogInfo($"Opened file: {filePath}");
        }
        catch (Exception ex) {
            Logger.LogError($"Error opening file: {ex.Message}");
        }
    }

}
