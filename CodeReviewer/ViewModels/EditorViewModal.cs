using System.Windows;
using CodeReviewer.Commands.FileCommands;
using CodeReviewer.Commands.HelpCommands;
using CodeReviewer.Commands.WindowCommands;
using CodeReviewer.Controllers;
using CodeReviewer.Logging;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;
using CodeReviewer.Services;
using CodeReviewer.Services.JsonServices;
using CodeReviewer.Views;
using Microsoft.Web.WebView2.Wpf;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. (Justification: Set by the constructor by setting the WindowTitle property)

namespace CodeReviewer.ViewModels;

/// <summary>
///     Represents the view model for the editor view in the application.
/// </summary>
public class EditorViewModal : ViewModelBase {
    
    private readonly IEditorWindowController _editorWindowController;
    private readonly IEditorModel _editorModel;
    private readonly ProjectDetailsService _projectDetailsService;
    private readonly FluentWindow _mainWindow;
    
    // ReSharper disable once FieldCanBeMadeReadOnly.Local (Justification: Set by the constructor by setting the WindowTitle property)
    private string _windowTitle;
    private string _infoText = "";

    public EditorViewModal(
        IEditorWindowController editorWindowController, 
        IEditorModel editorModel,
        ProjectDetailsService projectDetailsService,
        WebView2 webView, 
        FluentWindow mainWindow) {
        _editorWindowController = editorWindowController;
        _editorModel = editorModel;
        _projectDetailsService = projectDetailsService;
        _ = new WebViewInitializer(webView, InitializeEditorAsync);
        _mainWindow = mainWindow;
        
        _editorModel.FilePathChangedEvent += OnFileChanged;
        _editorModel.LanguageChangedEvent += OnProgrammingLanguageChanged;
        
        WindowTitle = _projectDetailsService.GetProjectTitle();
        _mainWindow.Title = WindowTitle;

        InitializeCommands();
    }

    public string WindowTitle {
        get => _windowTitle;
        private init => SetField(ref _windowTitle, value);
    }
    
    public string InfoText {
        get => _infoText;
        private set => SetField(ref _infoText, value);
    }

    public SaveFileCommand SaveFile { get; private set; }
    public OpenFileCommand OpenFile { get; private set; }
    public NewFileCommand NewFile { get; private set; }
    public NewWindowCommand OpenNewWindow { get; private set; }
    public ExitCommand Exit { get; private set; }
    public ToggleFullScreenCommand ToggleFullScreen { get; private set; }
    public OpenGitHubRepoCommand OpenGitHubRepo { get; private set; }
    public OpenAboutWindowCommand OpenAboutWindowCommand { get; private set; }
    public ViewLogCommand ViewLog { get; private set; }
    public ReportBugCommand ReportBug { get; private set; }

    private void InitializeCommands() {
        // File-related commands
        SaveFile = new SaveFileCommand(_editorWindowController, _editorModel);
        OpenFile = new OpenFileCommand(_editorWindowController, _editorModel);
        NewFile = new NewFileCommand(_editorWindowController, _editorModel);

        // Window-related commands
        OpenNewWindow = new NewWindowCommand();
        Exit = new ExitCommand();
        ToggleFullScreen = new ToggleFullScreenCommand(ToggleFullScreenHandler);

        // Help-related commands
        OpenGitHubRepo = new OpenGitHubRepoCommand(_projectDetailsService.ProjectDetails);
        OpenAboutWindowCommand = new OpenAboutWindowCommand((MainWindow)_mainWindow, _projectDetailsService.ProjectDetails);
        ViewLog = new ViewLogCommand(_editorWindowController, _editorModel);
        ReportBug = new ReportBugCommand(_projectDetailsService.ProjectDetails);
    }

    private async void InitializeEditorAsync(object? sender, EventArgs eventArgs) {
        IProgrammingLanguage startingLanguage = ProgrammingLanguages.Languages.FirstOrDefault()!;

        Logger.Instance.LogInfo("Initializing Monaco Editor");

        await _editorWindowController.CreateAsync();
        await _editorWindowController.AddCustomLanguageToEditorAsync(ProgrammingLanguages.GetProgrammingLanguageFromExtension(".crlog")!);
        await _editorWindowController.SetThemeAsync(ApplicationThemeManager.GetAppTheme());
        await _editorWindowController.SetLanguageAsync(startingLanguage);
        await _editorWindowController.SetContentAsync(startingLanguage.StartingCode);

        Logger.Instance.LogInfo("Finished initialization of Monaco Editor");

        _editorModel.CurrentLanguage = startingLanguage;
        InfoText = _editorModel.ToString();
    }

    private void OnFileChanged(object? sender, EventArgs e) {
        InfoText = _editorModel.ToString();
    }

    private void OnProgrammingLanguageChanged(object? sender, EventArgs e) {
        InfoText = _editorModel.ToString();
    }

    private void ToggleFullScreenHandler(object? sender, EventArgs e) {
        _mainWindow.WindowState =
            _mainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

}
