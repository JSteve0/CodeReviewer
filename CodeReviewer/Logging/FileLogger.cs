using System.IO;

namespace CodeReviewer.Logging;

public class FileLogger : ILogger {
    
    // ReSharper disable once InconsistentNaming
    private static readonly Lazy<FileLogger> _instance = new(() => new FileLogger());
    
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
    
    private static string CleanMessage(string message) {
        return message.Replace("\r\n", "\\n").Replace("\n", "\\n").Replace("\t", "\\t");
    }
    
    private static void Log(string message, string level) {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        const string fileName = "log.crlog";
        try {
            File.AppendAllText(fileName, $"[{timestamp}] [{level}] [{CleanMessage(message)}]\n"); 
        }
        catch (Exception ex) {
            ConsoleLogger.Instance.LogError(ex.ToString());
            throw;
        }
    }

}
