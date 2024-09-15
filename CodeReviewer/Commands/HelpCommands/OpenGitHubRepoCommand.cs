using System.Diagnostics;
using CodeReviewer.Services;

namespace CodeReviewer.Commands.HelpCommands;

public class OpenGitHubRepoCommand : CommandBase{

    public override void Execute(object? parameter) {
        Logger.LogInfo("Opening GitHub repository");
        
        string repositoryURL = ProjectDetailsService.Instance.GetRepositoryUrl();

        try {
            Process.Start(new ProcessStartInfo(repositoryURL) { UseShellExecute = true });
            Logger.LogInfo("GitHub repository opened successfully");
        } catch (Exception ex) {
            Logger.LogError($"Error opening GitHub repository: {ex.Message}");
        }
    }

}
