using CodeReviewer.Models.Languages;

namespace CodeReviewer.Models;

public interface IEditorModel {

    IProgrammingLanguage? CurrentLanguage { get; set; }
    string? FilePath { get; set; }
    event EventHandler? LanguageChangedEvent;
    event EventHandler? FilePathChangedEvent;
    string ToString();

}
