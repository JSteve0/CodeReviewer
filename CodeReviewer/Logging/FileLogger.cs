using System.IO;

namespace CodeReviewer.Logging;

public class FileLogger : ILogger {
    
    // ReSharper disable once InconsistentNaming
    private static readonly Lazy<FileLogger> _instance = new(() => new FileLogger());
    
    private FileLogger() { }
    
    public static FileLogger Instance => _instance.Value;

    public void LogError(string message) {
        Log(message, "ERROR");
    }

    public void LogInfo(string message) {
        Log(message, "INFO");
    }

    public void LogVerbose(string message) {
        Log(message, "VERBOSE");
    }

    public void LogWarning(string message) {
        Log(message, "WARNING");
    }
    
    private void Log(string message, string level) {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try {
            File.AppendAllText("log.txt", $"{timestamp} [{level}] {message}\n"); 
        }
        catch (Exception e) {
            ConsoleLogger.Instance.LogError(e.ToString());
            throw;
        }
    }

}
