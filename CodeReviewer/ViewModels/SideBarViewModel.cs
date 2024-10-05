using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using CodeReviewer.Commands.SideBarCommands;
using CodeReviewer.Logging;
using CodeReviewer.Managers;
using CodeReviewer.Models;
using CodeReviewer.Services.WebServices;
using OpenAI.Chat;
using Button = Wpf.Ui.Controls.Button;
using RichTextBox = Wpf.Ui.Controls.RichTextBox;

namespace CodeReviewer.ViewModels;

public class SideBarViewModel {
    
    private readonly RichTextBox _chatTextBox;
    private readonly ChatPanelManager _chatPanelManager;
    
    public SideBarViewModel(Button chatButton, StackPanel chatPanel, ColumnDefinition mainGrid, RichTextBox chatTextBox, GridSplitter gridSplitter) {
        ChatPanelManager chatPanelManager = new(chatPanel, mainGrid, gridSplitter);
        ChatButtonManager chatButtonManager = new(chatButton);
        
        ChatButtonCommand = new ChatButtonCommand(chatPanelManager, chatButtonManager);
        
        _chatPanelManager = chatPanelManager;

        _chatTextBox = chatTextBox;
        _chatTextBox.KeyDown += ChatTextBox_KeyDown;
    }
    
    public ChatButtonCommand ChatButtonCommand { get; private set; }
    
    private void ChatTextBox_KeyDown(object sender, KeyEventArgs e) {
        if (e.Key == Key.Enter) {
            string chatText = new TextRange(_chatTextBox.Document.ContentStart, _chatTextBox.Document.ContentEnd).Text;
            
            _chatPanelManager.AddChatMessage(chatText, MessageBubbleModel.SenderType.User);
            
            Task.Run(() => ProcessChatRequest(chatText));
            _chatTextBox.Document.Blocks.Clear();
        }
    }
    
    private async Task ProcessChatRequest(string chatText) {
        // Fire and forget, do not await in the ViewModel
        ChatCompletion? result = await ChatService.SendChatRequestAsync(chatText);

        try {  
            if (result != null) {
                Application.Current.Dispatcher.Invoke(() => {
                    Logger.Instance.LogVerbose($"Received Chat Response: {result}");
                    _chatPanelManager.AddChatMessage(result.ToString(), MessageBubbleModel.SenderType.ChatGPT);
                });
            } else {
                Application.Current.Dispatcher.Invoke(() => {
                    Logger.Instance.LogError("Failed to receive chat response.");
                });
            } 
        }
        catch (Exception e) {
            Logger.Instance.LogError($"Error processing chat request: {e.Message}\n{e.StackTrace}");
        }
    }

}
