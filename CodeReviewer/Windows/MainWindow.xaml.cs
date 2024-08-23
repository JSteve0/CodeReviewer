﻿using System.Windows;
using System.Windows.Controls;
using CodeReviewer.Controls;
using CodeReviewer.Models;
using CodeReviewer.Services;

namespace CodeReviewer.Windows;

public partial class MainWindow : Window
{
    private TextEditorControl? _activeTextEditorControl;
    private readonly TabManager _tabManager;
    private readonly SizeUpdater _sizeUpdater;

    public MainWindow()
    {
        InitializeComponent();
        _tabManager = new TabManager(Tabs);
        _sizeUpdater = new SizeUpdater(MainGrid, ActionsPanel, 200);

        _ = StartPeriodicUpdateAsync();
    }

    private async Task StartPeriodicUpdateAsync()
    {
        while (true)
        {
            OnTick();
            
            // Run at 144 FPS
            await Task.Delay(1000/144);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private void OnTick()
    {
        foreach (TextEditorControl editorControl in _tabManager.GetAllEditorControls())
        {
            editorControl.ProcessMessages();
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button) {
            switch (button.Name)
            {
                case "ClearAllTextButton":
                    _activeTextEditorControl?.QueueMessage(TextEditorWebManager.MessageType.Action, "clear");
                    break;
            }
        }
    }
    
    private void CreateNewFile(ProgrammingLanguage language)
    {
        var newTabName = _tabManager.GenerateUniqueTabName($"new.{LanguageManager.GetFileExtension(language)}");
        
        var textEditorControl = _tabManager.AddNewTab(newTabName);
        textEditorControl.SetLanguage(language);

        // TODO: Add user setting to open new tabs instantly
        // TODO: Add this to the tab manager?
        // Select and open the newly created tab if no other ones are open.
        if (_activeTextEditorControl == null) {
            _activeTextEditorControl = textEditorControl;
            Dispatcher.BeginInvoke(() => Tabs.SelectedIndex = 0);
        }

        _sizeUpdater.UpdateSizes(ActualWidth, ActualHeight);
        NewFileComboBox.SelectedIndex = -1;
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e) =>
        _sizeUpdater.UpdateSizes(ActualWidth, ActualHeight);

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        if (e.AddedItems[0] is not TabItem selectedTabItem) return;
        
        var tabTitle = selectedTabItem.Header.ToString();

        if (tabTitle == null) throw new InvalidOperationException();
        
        _activeTextEditorControl = _tabManager.GetEditorControl(tabTitle) ?? throw new InvalidOperationException();
        Title = tabTitle;
    }

    private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        if (e.AddedItems.Count <= 0 || sender is not ComboBox comboBox) return;
        
        var selectedItem = e.AddedItems[0] as ComboBoxItem;

        switch (selectedItem?.Name)
        {
            case "JavaScriptNewFileButton":
                CreateNewFile(ProgrammingLanguage.JavaScript);
                break;
            case "CSharpNewFileButton":
                CreateNewFile(ProgrammingLanguage.CSharp);
                break;
            case "JavaScriptButton":
                _activeTextEditorControl?.SetLanguage(ProgrammingLanguage.JavaScript);
                break;
            case "CSharpButton":
                _activeTextEditorControl?.SetLanguage(ProgrammingLanguage.CSharp);
                break;
            case "ClearAllTextButton":
                _activeTextEditorControl?.QueueMessage(TextEditorWebManager.MessageType.Action, "clear");
                break;
        }
            
        comboBox.SelectedIndex = -1;
    }
}
