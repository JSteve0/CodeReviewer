using CodeReviewer.Models;

namespace CodeReviewer.Commands.HelpCommands;

public class ReportBugCommand(ProjectDetailsModel projectDetailsModel) : OpenLinkCommandBase {

    public override void Execute(object? parameter) {
        Logger.LogInfo("Opening Issues in GitHub repository");
        
        string repositoryURL = projectDetailsModel.RepositoryURL + "/issues";
        OpenLink(repositoryURL);
    }

}
