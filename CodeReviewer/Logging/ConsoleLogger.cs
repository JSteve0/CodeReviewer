namespace CodeReviewer.Logging;
/// <summary>
/// A Singleton implementation of <see cref="ILogger"/> that logs messages to the console with timestamps.
/// </summary>
public class ConsoleLogger : ILogger {
    // ReSharper disable once InconsistentNaming
    private static readonly Lazy<ConsoleLogger> _instance = 
        new Lazy<ConsoleLogger>(() => new ConsoleLogger());

    /// <summary>
    /// Gets the singleton instance of <see cref="ConsoleLogger"/>.
    /// </summary>
    public static ConsoleLogger Instance => _instance.Value;

    // Private constructor to prevent instantiation
    private ConsoleLogger() {}

    private void Log(string level, string message) {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Console.WriteLine($"{timestamp} [{level}] {message}");
    }

    /// <inheritdoc/>
    public void LogInfo(string message) {
        Log("INFO", message);
    }

    /// <inheritdoc/>
    public void LogWarning(string message) {
        Log("WARNING", message);
    }

    /// <inheritdoc/>
    public void LogError(string message) {
        Log("ERROR", message);
    }
}
