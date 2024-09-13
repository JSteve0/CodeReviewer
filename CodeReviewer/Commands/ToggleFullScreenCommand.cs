using System.Windows.Input;

namespace CodeReviewer.Commands;

public class ToggleFullScreenCommand(EventHandler fullScreenEventHandler) : DelegateCommand {

    public override Key GestureKey { get; protected set; } = Key.F11;
    public override string GestureKeyText { get; protected set; } = "F11";

    public override void Execute(object? parameter) {
        fullScreenEventHandler.Invoke(this, EventArgs.Empty);
    }

}
