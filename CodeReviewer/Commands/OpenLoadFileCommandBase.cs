using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using Microsoft.Win32;

namespace CodeReviewer.Commands;

public class OpenLoadFileCommandBase(IEditorWindowController editorWindowController, IEditorModel editorModel) : NewLoadFileCommandBase(editorWindowController, editorModel) {
    
    public override void Execute(object? parameter) {
        var openFileDialog = new OpenFileDialog();

        bool? isFileSelected = openFileDialog.ShowDialog();

        if (isFileSelected != true) return;

        string fileName = openFileDialog.FileName;
        
        try {
            string fileText = File.ReadAllText(fileName, Encoding.UTF8);
            string escapedFileText = fileText.Replace("\"", "\\\"");
            string fileExtension = fileName.Split('.').Last();

            var newLanguage =
                ProgrammingLanguages.GetProgrammingLanguageFromExtension(fileExtension);

            CreateNewEditorFromFile(newLanguage, escapedFileText, fileName);
        }
        catch (Exception ex) {
            Console.WriteLine($"Error opening file: {ex.Message}");
        }
    }
    
}
