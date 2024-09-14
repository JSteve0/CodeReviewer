namespace CodeReviewer.Models.Languages;

public class CPlusPlusProgrammingLanguage : IProgrammingLanguage {

    public string DisplayName => "C++";

    public string Extension => "cpp";

    public string GetStartingCode() {
        return "#include <iostream>\n" +
               "\n" +
               "using std::cout;\n" +
               "using std::endl;\n" +
               "\n" +
               "int main()\n" +
               "{\n" +
               "\tcout << \"Hello World!\" << endl;\n" +
               "\treturn 0\n" +
               "}\n" +
               "";
    }

    public string MonacoName => "CPP";

    public override string ToString() {
        return MonacoName;
    }

}
