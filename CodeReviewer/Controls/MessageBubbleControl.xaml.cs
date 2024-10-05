using System.Windows;
using System.Windows.Controls;
using CodeReviewer.Models;

namespace CodeReviewer.Controls;

public partial class MessageBubbleControl : UserControl {

    public MessageBubbleModel MessageBubbleModel { get; }
    
    public MessageBubbleControl(string message, MessageBubbleModel.SenderType sender) {
        MessageBubbleModel = new MessageBubbleModel(message, sender, TimeOnly.FromDateTime(DateTime.Now));
        
        InitializeComponent(); 

        MessageBubble.HorizontalAlignment = sender == MessageBubbleModel.SenderType.User 
            ? HorizontalAlignment.Right 
            : HorizontalAlignment.Left;
        
        DataContext = this;
    }
    
    public Border GetMessageBubble => MessageBubble;

}

