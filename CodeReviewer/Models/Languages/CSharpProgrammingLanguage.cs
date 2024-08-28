﻿namespace CodeReviewer.Models.Languages;

public class CSharpProgrammingLanguage : IProgrammingLanguage {
    public string GetStartingCode() => "using System;\n\nclass HelloWorld {\n\n\tpublic static void Main(String[] args) {\n\t\tConsole.WriteLine(\"Hello World!);\n\t}\n\n}";
    public string Extension => "cs";
    public string Name => "CSharp";
    public ProgrammingLanguagesEnum GetProgrammingLanguageEnum() => ProgrammingLanguagesEnum.CSharp;
}
