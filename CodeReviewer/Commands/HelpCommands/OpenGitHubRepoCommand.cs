using CodeReviewer.Models;

namespace CodeReviewer.Commands.HelpCommands;

public class OpenGitHubRepoCommand(ProjectDetailsModel projectDetailsModel)
    : OpenLinkCommandBase {

    public override void Execute(object? parameter) {
        Logger.LogInfo("Opening GitHub repository");
        
        string repositoryURL = projectDetailsModel.RepositoryURL;
        OpenLink(repositoryURL);
    }

}
