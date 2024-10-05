using System.Windows;
using CodeReviewer.Models;
using CodeReviewer.Views;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace CodeReviewer.Commands.HelpCommands;

public class OpenAboutWindowCommand(MainWindow mainWindow, ProjectDetailsModel projectDetailsModel) : CommandBase {

    public override void Execute(object? parameter) {
        Logger.LogInfo("Opening About window");

        var aboutWindow = new MessageBox {
            Title = $"About {projectDetailsModel.Title}",
            Content = $"{projectDetailsModel.Description}\n\n" +
                      $"Developed by: {string.Join(", ", projectDetailsModel.Authors.Select(author => author.Name))}\n" +
                      $"Version: {projectDetailsModel.Version}\n" +
                      $"Repository URL: {projectDetailsModel.RepositoryURL}\n" +
                      $"License URL: {projectDetailsModel.LicenseURL}",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = mainWindow,
            FontSize = 13,
            MaxWidth = 800,
            Width = 800,
        };
        
        aboutWindow.Closed += (_, _) => Logger.LogInfo("Closed About window");

        aboutWindow.ShowDialogAsync();
    }

}
