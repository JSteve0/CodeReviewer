namespace CodeReviewer.Services;

public static class LanguageManager {
    public static string GetFileExtension(ProgrammingLanguage language) {
        return language switch {
            ProgrammingLanguage.JavaScript => "js",
            ProgrammingLanguage.CSharp => "cs",
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}
