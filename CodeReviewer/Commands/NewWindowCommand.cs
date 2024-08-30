using CodeReviewer.Windows;

namespace CodeReviewer.Commands;

public class NewWindowCommand : CommandBase{
    public override void Execute(object? parameter) {
        var newWindow = new MainWindow();
        newWindow.Show();
    }
}
