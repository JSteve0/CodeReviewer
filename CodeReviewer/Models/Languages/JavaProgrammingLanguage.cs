namespace CodeReviewer.Models.Languages;

public class JavaProgrammingLanguage : IProgrammingLanguage {
    public string Extension => "java"; 
    
    public string Name => "Java";

    public string GetStartingCode()
        => "public class Main {\n\n\tpublic static void main(String[] args) {\n\t\tSystem.out.println(\"Hello world!\");\n\t}\n\n}\n";
    
    public override string ToString() => Name;
    
}
