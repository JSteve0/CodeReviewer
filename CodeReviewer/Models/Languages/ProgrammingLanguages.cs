namespace CodeReviewer.Models.Languages;

public static class ProgrammingLanguages {
    public static readonly List<IProgrammingLanguage> Languages = [
        new CSharpProgrammingLanguage(),
        new JavaScriptProgrammingLanguage(),
        new TypeScriptProgrammingLanguage()
    ];

    public static IEnumerable<IProgrammingLanguage> GetAllLanguages() {
        return Languages;
    }

    public static IProgrammingLanguage? GetProgrammingLanguageFromExtension(string extension) {
        IProgrammingLanguage? language =
            Languages.FirstOrDefault(l => l.Extension.Equals(extension, StringComparison.OrdinalIgnoreCase));
        return language;
    }
}