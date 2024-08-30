using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CodeReviewer.Controllers;
using CodeReviewer.Models;

namespace CodeReviewer.Commands;

/// <summary>
///     Command to save the current content of the editor to a file.
/// </summary>
public class SaveFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel) : CommandBase {
    /// <summary>
    ///     Executes the command to save the editor's content to the file specified in the editor model.
    /// </summary>
    /// <param name="parameter">
    ///     Optional parameter for the command. Not used in this implementation and can be <c>null</c>.
    /// </param>
    public override async void Execute(object? parameter) {
        if (editorModel.FilePath == null || !File.Exists(editorModel.FilePath)) return;

        try {
            string output = await editorWindowController.GetContent();

            // Remove surrounding quotes if present
            if (output.StartsWith('\"') && output.EndsWith('\"')) output = output.Substring(1, output.Length - 2);

            output = Regex.Unescape(output);

            await File.WriteAllTextAsync(editorModel.FilePath, output, Encoding.UTF8);
        }
        catch (Exception ex) {
            Logger.LogError($"Error writing to file: {ex.Message}");
        }
    }
}