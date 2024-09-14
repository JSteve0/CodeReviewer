namespace CodeReviewer.Logging;

/// <summary>
///     Provides methods for logging messages.
/// </summary>
public interface ILogger {

    /// <summary>
    ///     Logs an error message.
    /// </summary>
    /// <param name="message">The error message to be logged.</param>
    void LogError(string message);

    /// <summary>
    ///     Logs an informational message.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    void LogInfo(string message);

    /// <summary>
    ///     Logs a warning message.
    /// </summary>
    /// <param name="message">The warning message to be logged.</param>
    void LogWarning(string message);

}
