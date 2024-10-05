namespace CodeReviewer.Models;

public class MessageBubbleModel {

    public string Message { get; set; }
    public SenderType Sender { get; set; }
    public TimeOnly Time { get; set; }
    
    public enum SenderType {
        User,
        ChatGPT
    }

    public MessageBubbleModel(string message, SenderType sender, TimeOnly time) {
        Message = message;
        Sender = sender;
        Time = time;
    }

}
