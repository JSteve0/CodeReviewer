namespace CodeReviewer.Logging;

/// <summary>
///     Provides methods for logging messages.
/// </summary>
public interface ILogger {
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}