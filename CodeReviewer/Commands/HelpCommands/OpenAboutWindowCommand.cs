using System.Windows;
using CodeReviewer.Models;
using CodeReviewer.Services;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace CodeReviewer.Commands.HelpCommands;

public class OpenAboutWindowCommand() : CommandBase {

    public override void Execute(object? parameter) {
        Logger.LogInfo("Opening About window");
        
        ProjectDetailsModel projectDetails = ProjectDetailsService.Instance.ProjectDetails;

        var aboutWindow = new MessageBox {
            Title = $"About {projectDetails.Title}",
            Content = $"{projectDetails.Description}\n\n" +
                      "Developed by: CodeReviewer Team\n" +
                      $"Version: {projectDetails.Version}\n" +
                      $"Repository URL: {projectDetails.RepositoryURL}\n" +
                      $"License URL: {projectDetails.LicenseURL}",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = Application.Current.MainWindow,
            FontSize = 13,
            MaxWidth = 800,
            Width = 800,
        };

        aboutWindow.ShowDialogAsync();
    }

}
