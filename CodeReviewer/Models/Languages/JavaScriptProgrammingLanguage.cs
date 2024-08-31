namespace CodeReviewer.Models.Languages;

public class JavaScriptProgrammingLanguage : IProgrammingLanguage {

    public string GetStartingCode() {
        return "function helloWorld() {\n\tconsole.log('Hello World!');\n}\n\nhelloWorld();\n";
    }

    public string Extension => "js";
    public string Name => "JavaScript";

    public override string ToString() {
        return Name;
    }

}
