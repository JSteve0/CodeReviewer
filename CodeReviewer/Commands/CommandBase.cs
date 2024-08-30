using System.Windows.Input;
using CodeReviewer.Logging;

namespace CodeReviewer.Commands;

/// <summary>
/// Abstract base class for implementing the <see cref="ICommand"/> interface.
/// Provides a default implementation for the <see cref="CanExecute"/> method and an event for when the command's ability to execute changes.
/// </summary>
public abstract class CommandBase : ICommand {
    protected readonly ILogger Logger = ConsoleLogger.Instance;
    
    /// <summary>
    /// Occurs when changes occur that affect whether or not the command can execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Determines whether the command can execute.
    /// By default, this method returns <c>true</c>.
    /// </summary>
    /// <param name="parameter">Data used by the command to determine if it can execute. This parameter is optional and can be <c>null</c>.</param>
    /// <returns><c>true</c> if the command can execute; otherwise, <c>false</c>.</returns>
    public virtual bool CanExecute(object? parameter) {
        return true;
    }

    /// <summary>
    /// Executes the command.
    /// This method must be implemented by derived classes to define the command's behavior.
    /// </summary>
    /// <param name="parameter">Data used by the command to execute. This parameter is optional and can be <c>null</c>.</param>
    public abstract void Execute(object? parameter);

    /// <summary>
    /// Raises the <see cref="CanExecuteChanged"/> event to indicate that the command's ability to execute may have changed.
    /// </summary>
    protected void OnCanExecuteChanged() {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
