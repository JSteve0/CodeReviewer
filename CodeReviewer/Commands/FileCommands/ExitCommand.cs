using System.Windows;

namespace CodeReviewer.Commands.FileCommands;

public class ExitCommand : CommandBase {

    public override void Execute(object? parameter) {
        Logger.LogInfo("Exiting application");
        
        Application.Current.Shutdown();
    }

}
