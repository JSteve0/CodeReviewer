using System.Windows.Input;

namespace CodeReviewer.Commands;

public abstract class DelegateCommand : CommandBase {

    public abstract Key GestureKey { get; protected set; }
    public abstract string GestureKeyText { get; protected set; }
    public virtual ModifierKeys GestureModifier { get; protected set; } = ModifierKeys.None;

}
