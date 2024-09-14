namespace CodeReviewer.Models.Languages;

public class JavaScriptProgrammingLanguage : IProgrammingLanguage {

    public string DisplayName => "JavaScript";

    public string Extension => "js";

    public string GetStartingCode() {
        return "function helloWorld() {\n\tconsole.log('Hello World!');\n}\n\nhelloWorld();\n";
    }

    public string MonacoName => "JavaScript";

    public override string ToString() {
        return DisplayName;
    }

}
