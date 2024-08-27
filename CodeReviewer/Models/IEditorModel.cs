using CodeReviewer.Models.Languages;

namespace CodeReviewer.Models;

public interface IEditorModel {
    ProgrammingLanguagesEnum? CurrentLanguage { get; set; }
    string? FilePath { get; set; }
    string ToString();
}
