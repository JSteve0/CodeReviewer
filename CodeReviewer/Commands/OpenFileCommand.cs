using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using Microsoft.Win32;

namespace CodeReviewer.Commands;

public class OpenFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel) : CommandBase {
    
    public override void Execute(object? parameter) {
        var openFileDialog = new OpenFileDialog();

        bool? result = openFileDialog.ShowDialog();

        if (result != true) return;

        string fileName = openFileDialog.FileName;
        
        try {
            string fileText = File.ReadAllText(fileName, Encoding.UTF8);
            string escapedFileText = fileText.Replace("\"", "\\\"");
            string fileExtension = fileName.Split('.').Last();

            _ = editorWindowController.SetContentAsync(escapedFileText);
            
            SetLanguage(ProgrammingLanguages.GetProgrammingLanguageFromExtension(fileExtension));

            editorModel.FilePath = fileName;
        }
        catch (Exception ex) {
            Console.WriteLine($"Error opening file: {ex.Message}");
        }
    }

    private void SetLanguage(ProgrammingLanguagesEnum? programmingLanguage) {
        editorModel.CurrentLanguage = programmingLanguage;
        _ = editorWindowController.SetLanguageAsync(programmingLanguage);
    }
    
}
