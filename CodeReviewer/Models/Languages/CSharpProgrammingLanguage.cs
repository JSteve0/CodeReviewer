namespace CodeReviewer.Models.Languages;

public class CSharpProgrammingLanguage : IProgrammingLanguage {

    public string DisplayName => "CSharp";

    public string Extension => "cs";

    public string GetStartingCode() {
        return
            "using System;\n\nclass HelloWorld {\n\n\tpublic static void Main(String[] args) {\n\t\tConsole.WriteLine(\"Hello World!\");\n\t}\n\n}";
    }

    public string MonacoName => "CSharp";

    public override string ToString() {
        return DisplayName;
    }

}
