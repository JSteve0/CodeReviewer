namespace CodeReviewer.Logging;

public static class Logger {

    public static ILogger Instance { get; set; } = FileLogger.Instance;

}
