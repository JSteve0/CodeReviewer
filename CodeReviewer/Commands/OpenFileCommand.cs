using System.IO;
using System.Text;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;
using Microsoft.Win32;

namespace CodeReviewer.Commands;

public class OpenFileCommand(IEditorWindowController editorWindowController, IEditorModel editorModel)
    : LoadFileCommandBase(editorWindowController, editorModel) {
    public override void Execute(object? parameter) {
        var openFileDialog = new OpenFileDialog();

        bool? isFileSelected = openFileDialog.ShowDialog();

        if (isFileSelected != true) return;

        string filePath = openFileDialog.FileName;

        try {
            string fileText = File.ReadAllText(filePath, Encoding.UTF8);
            string escapedFileText = fileText.Replace("\"", "\\\"");
            string fileExtension = Path.GetExtension(filePath)[1..];

            IProgrammingLanguage? newLanguage =
                ProgrammingLanguages.GetProgrammingLanguageFromExtension(fileExtension);

            CreateNewEditorFromFile(newLanguage, escapedFileText, filePath);
        }
        catch (Exception ex) {
            Console.WriteLine($"Error opening file: {ex.Message}");
        }
    }
}