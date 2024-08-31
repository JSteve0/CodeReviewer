namespace CodeReviewer.Models.Languages;

public interface IProgrammingLanguage {

    /// <summary>
    ///     Represents a programming language's file extension.
    /// </summary>
    string Extension { get; }

    /// <summary>
    ///     Represents the name of a programming language.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Retrieves the starting code for a programming language.
    /// </summary>
    /// <returns>The starting code for the programming language.</returns>
    string GetStartingCode();

    /// <summary>
    ///     Returns a string representation of the object.
    /// </summary>
    /// <returns>A string representation of the object.</returns>
    string ToString();

}
