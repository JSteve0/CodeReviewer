namespace CodeReviewer.Models.Languages;

public class JavaScriptProgrammingLanguage : IProgrammingLanguage {
    public string GetStartingCode() => "function helloWorld() {\n\tconsole.log('Hello World!');\n}\n";
    public string Extension => "js";
    public string Name => "JavaScript";
    public ProgrammingLanguagesEnum GetProgrammingLanguageEnum() => ProgrammingLanguagesEnum.JavaScript;
}
