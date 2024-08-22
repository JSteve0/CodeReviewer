using System.Windows.Controls;
using CodeReviewer.Models;

namespace CodeReviewer.Controls;

/// <summary>
/// A user control that hosts a WebView2 instance specifically for displaying a text editor.
/// </summary>
public partial class TextEditorControl : UserControl
{
    private readonly TextEditorWebManager _textEditorWebManager;

    public TextEditorControl()
    {
        InitializeComponent();
        _textEditorWebManager = new TextEditorWebManager("http://127.0.0.1:8000", WebView);
    }

    public void ProcessMessages() {
        _textEditorWebManager.ProcessMessages();
    }

    public void QueueMessage(TextEditorWebManager.MessageType messageType, string value)
    {
        _textEditorWebManager.QueueMessage(messageType, value);
    }

    public void SetLanguage(ProgrammingLanguage language)
    {
        QueueMessage(TextEditorWebManager.MessageType.Language, language.ToString());
    }
}

