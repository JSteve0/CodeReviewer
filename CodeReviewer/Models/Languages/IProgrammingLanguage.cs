namespace CodeReviewer.Models.Languages;

public interface IProgrammingLanguage {
    string GetStartingCode();
    string Extension { get; }
    string Name { get; }
    string ToString();
}
