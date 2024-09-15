using System.Windows.Input;
using CodeReviewer.Views;

namespace CodeReviewer.Commands.WindowCommands;

/// <summary>
///     Command to open a new editor window in the main application.
/// </summary>
public class NewWindowCommand : DelegateCommand {

    public override Key GestureKey { get; protected set; } = Key.N;
    public override string GestureKeyText { get; protected set; } = "Ctrl+Shift+N";
    public override ModifierKeys GestureModifier { get; protected set; } = ModifierKeys.Control | ModifierKeys.Shift;

    /// <summary>
    ///     Executes the command to create and display a new instance of the main application window.
    /// </summary>
    /// <param name="parameter">
    ///     Optional parameter for the command. Not used in this implementation and can be <c>null</c>.
    /// </param>
    public override void Execute(object? parameter) {
        Logger.LogInfo("Opening new window");
        
        var newWindow = new MainWindow();
        newWindow.Show();
    }

}
