using CodeReviewer.Models.Languages;

namespace CodeReviewer.Models;

public class EditorModel(EventHandler languageChangedEvent, EventHandler filePathChangedEvent) : IEditorModel {

    private IProgrammingLanguage? _currentLanguage;
    private string? _filePath;

    public IProgrammingLanguage? CurrentLanguage {
        get => _currentLanguage;
        set {
            _currentLanguage = value;
            languageChangedEvent.Invoke(this, EventArgs.Empty);
        }
    }

    public string? FilePath {
        get => _filePath;
        set {
            _filePath = value;
            filePathChangedEvent.Invoke(this, EventArgs.Empty);
        }
    }

    public override string ToString() {
        string languageText = CurrentLanguage?.ToString() ?? "Language is not supported";
        string fileText = FilePath ?? "No file loaded";

        return $"{languageText} | {fileText}";
    }

}
