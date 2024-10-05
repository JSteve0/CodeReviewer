using AllLanguages = CodeReviewer.Services.JsonServices.LanguagesService;

namespace CodeReviewer.Models.Languages;

public static class ProgrammingLanguages {

    /// <summary>
    ///     Provides a reference to all the programming languages.
    /// </summary>
    public static readonly List<LanguageModel> Languages = AllLanguages.Instance.Languages;

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
        return Languages.FirstOrDefault(l => l.Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase));
    }

}
