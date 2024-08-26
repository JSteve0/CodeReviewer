﻿using System.Windows;
using System.Windows.Threading;
using CodeReviewer.Commands;
using CodeReviewer.Controllers;
using CodeReviewer.Models;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;
using Wpf.Ui.Appearance;

namespace CodeReviewer.ViewModels;

public class EditorViewModal : ViewModelBase {
    private readonly IEditorWindowController _editorWindowController;
    private readonly IEditorModel _editorModel;

    private string _infoText = "";
    
    public string InfoText {
        get => _infoText;
        set {
            _infoText = value;
            OnPropertyChanged(nameof(InfoText));
        }
    }

    public SaveFileCommand SaveFile { get; private set; } = null!;
    public OpenFileCommand OpenFile { get; private set; } = null!;
    public NewFileCommand NewFile { get; private set; } = null!;

    public EditorViewModal(WebView2 webView, IEditorWindowController editorWindowController) {
        webView.NavigationCompleted += OnWebViewNavigationCompleted;
        webView.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, true);
        webView.SetCurrentValue(WebView2.DefaultBackgroundColorProperty, System.Drawing.Color.Transparent);
        webView.SetCurrentValue(
            WebView2.SourceProperty,
            new Uri(
                System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "MonacoEditor/index.html"
                )
            )
        );

        _editorWindowController = editorWindowController;

        _editorModel = new EditorModel(OnProgrammingLanguageChanged, OnFileChanged);
        
        InitializeCommands();
    }
    
    private async Task InitializeEditorAsync() {
        const ProgrammingLanguagesEnum startingLanguage = ProgrammingLanguagesEnum.JavaScript;
        
        await _editorWindowController.CreateAsync();
        await _editorWindowController.SetThemeAsync(ApplicationThemeManager.GetAppTheme());
        await _editorWindowController.SetLanguageAsync(startingLanguage);
        await _editorWindowController.SetContentAsync(ProgrammingLanguages.GetStartingCode(startingLanguage));

        _editorModel.CurrentLanguage = startingLanguage;
        InfoText = startingLanguage.ToString();
    }

    private void OnWebViewNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e) {
        DispatchAsync(InitializeEditorAsync);
    }

    private void InitializeCommands() {
        SaveFile = new SaveFileCommand(_editorWindowController, _editorModel);
        OpenFile = new OpenFileCommand(_editorWindowController, _editorModel);
        NewFile = new NewFileCommand(_editorWindowController, _editorModel);
    }
    
    private static DispatcherOperation<TResult> DispatchAsync<TResult>(Func<TResult> callback) {
        return Application.Current.Dispatcher.InvokeAsync(callback);
    }
    
    private void OnProgrammingLanguageChanged(object? sender, EventArgs e) {
        InfoText = _editorModel.CurrentLanguage.ToString() + " | " + _editorModel.FilePath;
    }
    
    private void OnFileChanged(object? sender, EventArgs e) {
        InfoText = _editorModel.CurrentLanguage.ToString() + " | " + _editorModel.FilePath;
    }
}
