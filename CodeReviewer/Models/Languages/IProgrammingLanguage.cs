namespace CodeReviewer.Models.Languages;

public interface IProgrammingLanguage {

    /// <summary>
    ///     Represents a programming language's file extension.
    /// </summary>
    List<string> Extensions { get; }

    /// <summary>
    ///     Represents the name of a programming language.
    /// </summary>
    string? MonacoName { get; }

    string Name { get; }
    
    string StartingCode { get; }
    
    string ToString();

}
