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

    /// <inheritdoc />
    public void LogInfo(string message) {
        Log("INFO", message);
    }

    /// <inheritdoc />
    public void LogWarning(string message) {
        Log("WARNING", message);
    }

    /// <inheritdoc />
    public void LogError(string message) {
        Log("ERROR", message);
    }

    private void Log(string level, string message) {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Console.WriteLine($"{timestamp} [{level}] {message}");
    }
}