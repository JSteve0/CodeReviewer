using CodeReviewer.Models.Languages;

namespace CodeReviewer.Models;

public interface IEditorModel {

    IProgrammingLanguage? CurrentLanguage { get; set; }
    string? FilePath { get; set; }
    string ToString();

}
