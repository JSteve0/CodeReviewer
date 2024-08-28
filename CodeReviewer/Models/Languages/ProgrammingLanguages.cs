namespace CodeReviewer.Models.Languages;

public static class ProgrammingLanguages
{
    private static readonly List<IProgrammingLanguage> Languages = [
        new JavaScriptProgrammingLanguage(),
        new CSharpProgrammingLanguage()
    ];
    
    public static IEnumerable<IProgrammingLanguage> GetAllLanguages() => Languages;

    public static string GetStartingCode(ProgrammingLanguagesEnum programmingLanguage) {
        IProgrammingLanguage? language = Languages.FirstOrDefault(l => l.GetProgrammingLanguageEnum() == programmingLanguage);
        if (language == null)
            throw new ArgumentOutOfRangeException(nameof(programmingLanguage), programmingLanguage, null);
        
        return language.GetStartingCode();
    }

    public static string GetExtension(ProgrammingLanguagesEnum programmingLanguage) {
        IProgrammingLanguage? language = Languages.FirstOrDefault(l => l.GetProgrammingLanguageEnum() == programmingLanguage);
        if (language == null)
            throw new ArgumentOutOfRangeException(nameof(programmingLanguage), programmingLanguage, null);
        
        return language.Extension;
    }

    public static ProgrammingLanguagesEnum? GetProgrammingLanguageFromExtension(string extension) {
        IProgrammingLanguage? language = Languages.FirstOrDefault(l => l.Extension.Equals(extension, StringComparison.OrdinalIgnoreCase));
        return language?.GetProgrammingLanguageEnum();
    }
}

