using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CodeReviewer.Controls;
using CodeReviewer.Models;
using RichTextBox = Wpf.Ui.Controls.RichTextBox;

namespace CodeReviewer.Managers;

public class ChatPanelManager {

    private readonly ScrollViewer _scrollViewer;
    private readonly StackPanel _chatPanel;
    private readonly ColumnDefinition _sideBarColumn;
    private GridLength _previousWidth;

    public ChatPanelManager(StackPanel chatPanel, ColumnDefinition sideBarColumn, GridSplitter gridSplitter) {
        _chatPanel = chatPanel;
        _sideBarColumn = sideBarColumn;
        _previousWidth = new GridLength(300, GridUnitType.Pixel); // Default width
        _sideBarColumn.Width = _previousWidth;
        _scrollViewer = chatPanel.Parent as ScrollViewer ?? throw new InvalidOperationException("ChatPanel must be inside a ScrollViewer");
        
        gridSplitter.DragCompleted += OnResize;
        
        HideChatWindow();
    }

    public bool IsChatWindowVisible => _chatPanel.Visibility == Visibility.Visible;

    public void ShowChatWindow() {
        _chatPanel.Visibility = Visibility.Visible;

        // Restore the previous width
        _sideBarColumn.Width = _previousWidth;
        _scrollViewer.Width = _previousWidth.Value;
        _chatPanel.Width = _previousWidth.Value;
    }

    public void HideChatWindow() {
        _chatPanel.Visibility = Visibility.Collapsed;

        // Store the width before collapsing
        _previousWidth = _sideBarColumn.Width;
        _sideBarColumn.Width = new GridLength(0);
        _scrollViewer.Width = 0;
    }

    private void OnResize(object sender, DragCompletedEventArgs e) {
        _previousWidth = _sideBarColumn.Width;
        ShowChatWindow();
        
        foreach (UIElement child in _chatPanel.Children) {
            // cast to RichTextBox to access the Width property
            if (child is not MessageBubbleControl messageBubble)
                continue;
            
            messageBubble.GetMessageBubble.MaxWidth = _sideBarColumn.Width.Value < 300 
                ? _sideBarColumn.Width.Value 
                : Math.Max(300, _previousWidth.Value * 0.6);
        }
    }
    
    public void AddChatMessage(string message, MessageBubbleModel.SenderType sender) {
        // Insert into the second to last position to keep the input box at the bottom
        _chatPanel.Children.Insert(Math.Max(_chatPanel.Children.Count - 1, 0), new MessageBubbleControl(message, sender));
    }
}
