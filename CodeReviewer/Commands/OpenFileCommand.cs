using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using Microsoft.Win32;

namespace CodeReviewer.Commands;

public class OpenFileCommand(EditorWindowController editorWindowController, EditorModel editorModel) : CommandBase {
    
    public override void Execute(object? parameter)
    {
        var openFileDialog = new OpenFileDialog();

        bool? result = openFileDialog.ShowDialog();

        if (result != true) return;

        string fileName = openFileDialog.FileName;
        
        try
        {
            // Read the file content with UTF-8 encoding
            string fileText = File.ReadAllText(fileName, Encoding.UTF8);
            string escapedFileText = fileText.Replace("\"", "\\\"");

            // Log file name and content
            Console.WriteLine("Opened File:");
            Console.WriteLine(fileName);
            Console.WriteLine("File Content:");
            Console.WriteLine(escapedFileText);

            // Set the content in the editor asynchronously
            _ = editorWindowController.SetContentAsync(escapedFileText);

            // Update the model with the file path
            editorModel.FilePath = fileName;
        }
        catch (Exception ex)
        {
            // Log any errors that occur during file reading
            Console.WriteLine($"Error opening file: {ex.Message}");
        }
    }
    
}
