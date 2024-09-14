namespace CodeReviewer.Logging;

/// <summary>
///     A Singleton implementation of <see cref="ILogger" /> that logs messages to the console with timestamps.
/// </summary>
public class ConsoleLogger : ILogger {

    // ReSharper disable once InconsistentNaming
    private static readonly Lazy<ConsoleLogger> _instance = new(() => new ConsoleLogger());

    // Private constructor to prevent instantiation
    private ConsoleLogger() { }

    /// <summary>
    ///     Gets the singleton instance of <see cref="ConsoleLogger" />.
    /// </summary>
    public static ConsoleLogger Instance => _instance.Value;

    /// <summary>
    ///     Logs an error message to the console with a timestamp.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void LogError(string message) {
        Log("ERROR", message);
    }

    /// <summary>
    ///     Logs an information message to the console with a timestamp.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void LogInfo(string message) {
        Log("INFO", message);
    }

    /// <summary>
    ///     Logs a warning message to the console with a timestamp.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void LogWarning(string message) {
        Log("WARNING", message);
    }

    private void Log(string level, string message) {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Console.WriteLine($"{timestamp} [{level}] {message}");
    }

}
