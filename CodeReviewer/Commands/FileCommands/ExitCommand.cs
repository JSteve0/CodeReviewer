using System.Windows;

namespace CodeReviewer.Commands.FileCommands;

public class ExitCommand : CommandBase {

    public override void Execute(object? parameter) {
        Logger.LogInfo("Exiting application");
        
        // TODO: Make this only close the main window or provide info somewhere that this will close the entire application
        Application.Current.Shutdown();
    }

}
