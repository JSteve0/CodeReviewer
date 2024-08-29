namespace CodeReviewer.Models.Languages;

public interface IProgrammingLanguage {
    string Extension { get; }
    string Name { get; }
    string GetStartingCode();
    string ToString();
}