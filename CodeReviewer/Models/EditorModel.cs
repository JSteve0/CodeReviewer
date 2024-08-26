namespace CodeReviewer.Models;

public class EditorModel(EventHandler languageChangedEvent, EventHandler filePathChangedEvent) : IEditorModel {
    private ProgrammingLanguagesEnum? _currentLanguage;
    private string? _filePath;
    
    public ProgrammingLanguagesEnum? CurrentLanguage { get => _currentLanguage;
        set {
            _currentLanguage = value;
            languageChangedEvent.Invoke(this, EventArgs.Empty);
        }
    }
    public string? FilePath { get => _filePath;
        set {
            _filePath = value;
            filePathChangedEvent.Invoke(this, EventArgs.Empty);
        }
    }
}
