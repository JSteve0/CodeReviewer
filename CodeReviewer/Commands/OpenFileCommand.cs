using System.IO;
using System.Net;
using CodeReviewer.Controllers;
using CodeReviewer.ViewModels;
using Microsoft.Win32;

namespace CodeReviewer.Commands;

public class OpenFileCommand(EditorWindowController editorWindowController) : CommandBase {
    
    public override void Execute(object? parameter) {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        bool? result = openFileDialog.ShowDialog();

        if (result == true) {
            string fileName = openFileDialog.FileName;
            
            Console.WriteLine(fileName);
            Console.WriteLine(File.ReadAllText(fileName));

            _ = editorWindowController.SetContentAsync(File.ReadAllText(fileName));
        }
    }
    
}
