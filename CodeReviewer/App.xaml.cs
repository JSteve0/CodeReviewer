using System.Windows;
using CodeReviewer.Commands;
using CodeReviewer.Controllers;
using CodeReviewer.Logging;
using CodeReviewer.Models;
using CodeReviewer.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeReviewer;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
// ReSharper disable once RedundantExtendsListEntry
public partial class App : Application {
    private IHost? _host;

    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);

        _host = CreateHostBuilder().Build();
        _host.Start();

        IServiceProvider serviceProvider = _host.Services;

        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private IHostBuilder CreateHostBuilder() {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) => {
                // Register services
                services.AddTransient<IEditorModel, EditorModel>();
                services.AddTransient<IEditorWindowController, EditorWindowController>();
                services.AddSingleton<ILogger, ConsoleLogger>();

                // Register commands
                services.AddTransient<NewFileCommand>();
                services.AddTransient<OpenFileCommand>();
                services.AddTransient<SaveFileCommand>();
                services.AddTransient<NewWindowCommand>();

                // Register the main window
                services.AddTransient<MainWindow>();
            });
    }

    protected override void OnExit(ExitEventArgs e) {
        _host?.Dispose();
        base.OnExit(e);
    }
}
