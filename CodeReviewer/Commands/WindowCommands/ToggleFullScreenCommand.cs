using System.Windows.Input;

namespace CodeReviewer.Commands.WindowCommands;

/// <summary>
///     Command to toggle fullscreen on the current editor
/// </summary>
/// <param name="fullScreenEventHandler">Fullscreen event that gets triggered when the command is executed</param>
public class ToggleFullScreenCommand(EventHandler fullScreenEventHandler) : DelegateCommand {

    public override Key GestureKey { get; protected set; } = Key.F11;
    public override string GestureKeyText { get; protected set; } = "F11";

    public override void Execute(object? parameter) {
        Logger.LogInfo("Toggling fullscreen");
        fullScreenEventHandler.Invoke(this, EventArgs.Empty);
    }

}
