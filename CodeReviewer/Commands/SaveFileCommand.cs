using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CodeReviewer.Controllers;
using CodeReviewer.Models;

namespace CodeReviewer.Commands;

public class SaveFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel) : CommandBase {
    public override async void Execute(object? parameter) {
        // TODO Add error handling
        if (editorModel.FilePath == null || !File.Exists(editorModel.FilePath)) return;

        try {
            string output = await editorWindowController.GetContent();

            // Remove the surrounding quotes if present
            if (output.StartsWith('\"') && output.EndsWith('\"')) output = output.Substring(1, output.Length - 2);

            output = Regex.Unescape(output);

            Console.WriteLine(output);

            await File.WriteAllTextAsync(editorModel.FilePath, output, Encoding.UTF8);
        }
        catch (Exception ex) {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
}