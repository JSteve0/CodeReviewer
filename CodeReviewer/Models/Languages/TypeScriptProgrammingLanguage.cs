﻿namespace CodeReviewer.Models.Languages;

public class TypeScriptProgrammingLanguage : IProgrammingLanguage {
    public string GetStartingCode() => "function helloWorld() {\n\tconst textToPrint: String = \"\";\n\tconsole.log(textToPrint);\n}\n\nhelloWorld();";

    public string Extension => "ts";
    public string Name => "TypeScript";
    public override string ToString() => Name;
}
