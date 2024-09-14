namespace CodeReviewer.Models.Languages;

public class JavaProgrammingLanguage : IProgrammingLanguage {

    public string DisplayName => "Java";

    public string Extension => "java";

    public string GetStartingCode() {
        return
            "public class Main {\n\n\tpublic static void main(String[] args) {\n\t\tSystem.out.println(\"Hello world!\");\n\t}\n\n}\n";
    }

    public string MonacoName => "Java";

    public override string ToString() {
        return DisplayName;
    }

}
