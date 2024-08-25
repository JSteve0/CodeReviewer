using CodeReviewer.Controllers;

namespace CodeReviewer.Commands;

public class SaveFileCommand(EditorWindowController editorWindowController) : CommandBase {
    
    public override async void Execute(object? parameter) {
        string output = await editorWindowController.GetContent();
        Console.WriteLine(output);
    }
    
}
