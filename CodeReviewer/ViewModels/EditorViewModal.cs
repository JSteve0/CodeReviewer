﻿using System.Windows;
using CodeReviewer.Commands;
using CodeReviewer.Controllers;
using CodeReviewer.Logging;
using CodeReviewer.Models;
using CodeReviewer.Models.Languages;
using CodeReviewer.Services;
using Microsoft.Web.WebView2.Wpf;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CodeReviewer.ViewModels;

/// <summary>
///     Represents the view model for the editor view in the application.
/// </summary>
public class EditorViewModal : ViewModelBase {

    private readonly IEditorModel _editorModel;
    private readonly IEditorWindowController _editorWindowController;
    private readonly FluentWindow _mainWindow;

    private string _infoText = "";

    public EditorViewModal(WebView2 webView, IEditorWindowController editorWindowController, FluentWindow mainWindow) {
        _editorWindowController = editorWindowController;
        _editorModel = new EditorModel(OnProgrammingLanguageChanged, OnFileChanged);
        _ = new WebViewInitializer(webView, InitializeEditorAsync);
        _mainWindow = mainWindow;

        InitializeCommands();
    }

    public string InfoText {
        get => _infoText;
        private set {
            _infoText = value;
            OnPropertyChanged();
        }
    }

    public SaveFileCommand SaveFile { get; private set; } = null!;
    public OpenFileCommand OpenFile { get; private set; } = null!;
    public NewFileCommand NewFile { get; private set; } = null!;
    public NewWindowCommand OpenNewWindow { get; private set; } = null!;
    public ExitCommand Exit { get; private set; } = null!;
    public ToggleFullScreenCommand ToggleFullScreen { get; private set; } = null!;

    private void InitializeCommands() {
        SaveFile = new SaveFileCommand(_editorWindowController, _editorModel);
        OpenFile = new OpenFileCommand(_editorWindowController, _editorModel);
        NewFile = new NewFileCommand(_editorWindowController, _editorModel);
        OpenNewWindow = new NewWindowCommand();
        Exit = new ExitCommand();
        ToggleFullScreen = new ToggleFullScreenCommand(ToggleFullScreenHandler);
    }

    private void OnProgrammingLanguageChanged(object? sender, EventArgs e) {
        InfoText = _editorModel.ToString();
    }

    private void OnFileChanged(object? sender, EventArgs e) {
        InfoText = _editorModel.ToString();
    }

    private async void InitializeEditorAsync(object? sender, EventArgs eventArgs) {
        IProgrammingLanguage startingLanguage = ProgrammingLanguages.Languages.FirstOrDefault()!;

        ConsoleLogger.Instance.LogInfo("Initializing Monaco Editor");

        await _editorWindowController.CreateAsync();
        await _editorWindowController.SetThemeAsync(ApplicationThemeManager.GetAppTheme());
        await _editorWindowController.SetLanguageAsync(startingLanguage);
        await _editorWindowController.SetContentAsync(startingLanguage.GetStartingCode());

        ConsoleLogger.Instance.LogInfo("Finished initialization of Monaco Editor");

        _editorModel.CurrentLanguage = startingLanguage;
        InfoText = _editorModel.ToString();
    }

    private void ToggleFullScreenHandler(object? sender, EventArgs e) => _mainWindow.WindowState =
        _mainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

}
