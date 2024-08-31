namespace CodeReviewer.Models.Languages;

public static class ProgrammingLanguages {

    /// <summary>
    ///     Provides a reference to all the programming languages.
    /// </summary>
    public static readonly List<IProgrammingLanguage> Languages = [
        new CSharpProgrammingLanguage(),
        new JavaProgrammingLanguage(),
        new JavaScriptProgrammingLanguage(),
        new TypeScriptProgrammingLanguage()
    ];

    /// <summary>
    ///     Retrieves all the programming languages available.
    /// </summary>
    /// <returns>An IEnumerable containing all the programming languages.</returns>
    public static IEnumerable<IProgrammingLanguage> GetAllLanguages() {
        return Languages;
    }

    /// <summary>
    ///     Retrieves the programming language associated with the given file extension.
    /// </summary>
    /// <param name="extension">The file extension.</param>
    /// <returns>
    ///     The programming language associated with the given file extension, or null if no programming language is
    ///     found.
    /// </returns>
    public static IProgrammingLanguage? GetProgrammingLanguageFromExtension(string extension) {
        IProgrammingLanguage? language =
            Languages.FirstOrDefault(l => l.Extension.Equals(extension, StringComparison.OrdinalIgnoreCase));
        return language;
    }

}
