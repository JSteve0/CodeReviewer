using System.Windows;

namespace CodeReviewer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);
        
        Logging.ConsoleLogger.Instance.LogInfo("Application Starting");
    }

    protected override void OnExit(ExitEventArgs e) {
        base.OnExit(e);
        Logging.ConsoleLogger.Instance.LogInfo($"Application Closing with exit code: {e.ApplicationExitCode}");
    }
}
