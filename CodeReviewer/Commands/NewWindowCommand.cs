using CodeReviewer.Windows;

namespace CodeReviewer.Commands;

/// <summary>
///     Command to open a new window of the main application.
/// </summary>
public class NewWindowCommand : CommandBase {
    /// <summary>
    ///     Executes the command to create and display a new instance of the main application window.
    /// </summary>
    /// <param name="parameter">
    ///     Optional parameter for the command. Not used in this implementation and can be <c>null</c>.
    /// </param>
    public override void Execute(object? parameter) {
        var newWindow = new MainWindow();
        newWindow.Show();
    }
}