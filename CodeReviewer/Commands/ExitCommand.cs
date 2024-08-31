using System.Windows;

namespace CodeReviewer.Commands;

public class ExitCommand : CommandBase {

    public override void Execute(object? parameter) {
        Application.Current.Shutdown();
    }

}
