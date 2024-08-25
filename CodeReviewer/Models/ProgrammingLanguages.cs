namespace CodeReviewer.Models;

public static class ProgrammingLanguages {

    public static string GetStartingCode(ProgrammingLanguagesEnum programmingLanguage) {
        return programmingLanguage switch {
            ProgrammingLanguagesEnum.JavaScript => "function helloWorld() {{\n\tconsole.log('Hello World!');\n}}\n",
            ProgrammingLanguagesEnum.CSharp => "using System;\n\nclass HelloWorld\n\n\tpublic static void Main(String[] args) {\n\t\tConsole.WriteLine(\"Hello World!);\n\t}\n\n}",
            _ => throw new ArgumentOutOfRangeException(nameof(programmingLanguage), programmingLanguage, null)
        };
    }

    public static string GetExtension(ProgrammingLanguagesEnum programmingLanguage) {
        return programmingLanguage switch {
            ProgrammingLanguagesEnum.JavaScript => "js",
            ProgrammingLanguagesEnum.CSharp => "cs",
            _ => throw new ArgumentOutOfRangeException(nameof(programmingLanguage), programmingLanguage, null)
        };
    }
    
}
