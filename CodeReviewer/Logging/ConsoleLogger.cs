namespace CodeReviewer.Logging;

/// <summary>
///     A Singleton implementation of <see cref="ILogger" /> that logs messages to the console with timestamps.
/// </summary>
public class ConsoleLogger : ILogger {

    // ReSharper disable once InconsistentNaming
    private static readonly Lazy<ConsoleLogger> _instance = new(() => new ConsoleLogger());

    /// <summary>
    ///     Gets the singleton instance of <see cref="ConsoleLogger" />.
    /// </summary>
    public static ConsoleLogger Instance => _instance.Value;

    /// <summary>
    ///     Logs an error message to the console with a timestamp.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void LogError(string message) {
        Log("ERROR", message, ConsoleColor.Red);
    }

    /// <summary>
    ///     Logs an information message to the console with a timestamp.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void LogInfo(string message) {
        Log("INFO", message, ConsoleColor.Green);
    }

    public void LogVerbose(string message) {
        Log("VERBOSE", message, ConsoleColor.Gray);
    }

    /// <summary>
    ///     Logs a warning message to the console with a timestamp.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public void LogWarning(string message) {
        Log("WARNING", message, ConsoleColor.Yellow);
    }
    
    private static string CleanMessage(string message) {
        return message.Replace("\r\n", "\\n").Replace("\n", "\\n").Replace("\t", "\\t");
    }

    private static void Log(string level, string message, ConsoleColor consoleColor) {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        
        Console.ForegroundColor = consoleColor;
        Console.WriteLine($"[{timestamp}] [{level}] [{CleanMessage(message)}]");
        Console.ResetColor();
    }

}
