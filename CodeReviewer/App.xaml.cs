using System.Windows;
using CodeReviewer.Controllers;
using CodeReviewer.Logging;
using CodeReviewer.Models;
using CodeReviewer.Services;
using CodeReviewer.Services.JsonServices;
using CodeReviewer.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeReviewer;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
// ReSharper disable once RedundantExtendsListEntry
public partial class App : Application {

    private IHost? _host;
    
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    private IHostBuilder CreateHostBuilder() {
        return Host.CreateDefaultBuilder()
                   .ConfigureServices((_, services) => {
                       // Register services
                       services.AddTransient<IEditorModel, EditorModel>();
                       services.AddTransient<IEditorWindowController, EditorWindowController>();
                       services.AddSingleton<ILogger, ConsoleLogger>();
                       services.AddSingleton<ProjectDetailsService>();

                       // Register the main window
                       services.AddTransient<MainWindow>();
                   });
    }

    protected override void OnExit(ExitEventArgs e) {
        _host?.Dispose();
        base.OnExit(e);
    }

    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);

        _host = CreateHostBuilder().Build();
        _host.Start();

        ServiceProvider = _host.Services;

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

}
