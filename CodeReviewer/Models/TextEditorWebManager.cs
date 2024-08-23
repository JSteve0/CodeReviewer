using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;

namespace CodeReviewer.Models;

/// <summary>
/// Manages communication with the web-based text editor via WebView2.
/// </summary>
public class TextEditorWebManager
{
    private readonly WebView2 _webView;
    private readonly Queue<(MessageType MessageType, string Value)> _messageQueue = new();
    private DateTime? _nextUpdateTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextEditorWebManager"/> class.
    /// </summary>
    /// <param name="url">The URL of the web-based text editor.</param>
    /// <param name="webView2">An optional WebView2 instance. If not provided, a new instance will be created.</param>
    public TextEditorWebManager(string url, WebView2? webView2 = null)
    {
        _webView = webView2 ?? new WebView2();
        _webView.Source = new Uri(url);

        _webView.CoreWebView2InitializationCompleted += (_, _) =>
            // CoreWebView2 is null until initialized, so this must be inside CoreWebView2InitializationCompleted
            _webView.CoreWebView2.WebMessageReceived += ProcessReceivedMessages;
    }

    /// <summary>
    /// Processes messages in the queue and sends them to the WebView2 instance.
    /// </summary>
    public void ProcessMessagesToSend()
    {
        if (_messageQueue.Count == 0 || _webView.CoreWebView2 == null) return;

        _nextUpdateTime ??= DateTime.Now.AddMilliseconds(200);

        if (DateTime.Now < _nextUpdateTime) return;

        var message = _messageQueue.Dequeue();
        SendMessage(message.MessageType, message.Value);
    }

    /// <summary>
    /// Process messages received from the frontend.
    /// </summary>
    private void ProcessReceivedMessages(object? sender, CoreWebView2WebMessageReceivedEventArgs args) {
        Console.WriteLine(args.WebMessageAsJson);
    }

    /// <summary>
    /// Sends a message to the WebView2 instance in JSON format.
    /// </summary>
    /// <param name="messageType">The type of the message to be sent.</param>
    /// <param name="value">The value associated with the message type.</param>
    private void SendMessage(MessageType messageType, string value)
    {
        var jsonMessage = $"{{\"{messageType.ToString().ToLower()}\": \"{value.ToLower()}\"}}";
        _webView.CoreWebView2?.PostWebMessageAsJson(jsonMessage);
    }

    /// <summary>
    /// Queues a message to be sent to the WebView2 instance.
    /// </summary>
    /// <param name="messageType">The type of the message to be queued.</param>
    /// <param name="value">The value to be associated with the message type.</param>
    public void QueueMessage(MessageType messageType, string value)
    {
        _messageQueue.Enqueue((messageType, value));
    }

    /// <summary>
    /// Queues a language change message for the text editor.
    /// </summary>
    /// <param name="language">The language to set in the text editor.</param>
    public void SetEditorLanguage(ProgrammingLanguage language)
    {
        QueueMessage(MessageType.Language, language.ToString());
    }

    /// <summary>
    /// Represents the types of messages used in JSON communication with the WebView2 instance.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Represents a message to change the language of the text editor.
        /// </summary>
        Language,

        /// <summary>
        /// Represents a message to perform an action as specified in the JSON message.
        /// </summary>
        Action
    }
}

