using System.Diagnostics;

namespace CodeReviewer.Commands.HelpCommands;

public abstract class OpenLinkCommandBase : CommandBase {
    
    protected void OpenLink(string url) {
        try {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            Logger.LogInfo($"{url} opened successfully");
        } catch (Exception ex) {
            Logger.LogError($"Error opening link: {ex}");
        }
    }

}
