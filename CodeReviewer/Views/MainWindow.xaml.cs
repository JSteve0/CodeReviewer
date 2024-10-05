using System.Windows;
using CodeReviewer.Controllers;
using CodeReviewer.Logging;
using CodeReviewer.Models;
using CodeReviewer.Services;
using CodeReviewer.Services.JsonServices;
using CodeReviewer.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;

namespace CodeReviewer.Views;

public partial class MainWindow {

    public MainWindow() {
        InitializeComponent();
        
        // Run a background task to get the response from the OpenAI API
        Task.Run(() => {
            //ChatCompletion results = OpenAIService.ChatClient.CompleteChat("Give me starter code for a C program that takes in CLI arguments and prints them to the console. Only output the code and nothing else.");
            
            // Once the background task completes, print the result
            //Console.WriteLine(results);
        });
        
        Logger.Instance = App.ServiceProvider.GetRequiredService<ILogger>();
        
        var editorViewModal = new EditorViewModal(
            new EditorWindowController(WebView),
            App.ServiceProvider.GetRequiredService<IEditorModel>(),
            App.ServiceProvider.GetRequiredService<ProjectDetailsService>(),
            WebView,
            this
        );
        
        DataContext = editorViewModal;
        
        var sideBarViewModel = new SideBarViewModel(ChatButton, ChatPanel, SideBarWindowColumnDefinition, ChatTextBox, GridSplitter);
        ChatPanel.DataContext = sideBarViewModel;
        ChatButton.DataContext = sideBarViewModel;
    }

}
