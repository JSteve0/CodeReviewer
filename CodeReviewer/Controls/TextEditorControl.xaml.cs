using System.IO;
using System.Reflection;
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
        
        string exePath = Assembly.GetExecutingAssembly().Location;
        
        string exeDirectory = Path.GetDirectoryName(exePath) ?? throw new InvalidOperationException();
        
        _textEditorWebManager = new TextEditorWebManager($"file:///{exeDirectory}/MonacoEditor/index.html", WebView);
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
        _textEditorWebManager.SetEditorLanguage(language);
    }
}

