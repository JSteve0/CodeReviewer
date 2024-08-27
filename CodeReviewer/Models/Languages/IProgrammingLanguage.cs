namespace CodeReviewer.Models.Languages;

public interface IProgrammingLanguage {
    string GetStartingCode();
    string GetExtension();
    ProgrammingLanguagesEnum GetProgrammingLanguageEnum();
}
