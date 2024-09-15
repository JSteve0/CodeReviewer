namespace CodeReviewer.Models.Languages;

public class PythonProgrammingLanguage : IProgrammingLanguage {

    public string Extension => "py";
    public string MonacoName => "Python";
    public string DisplayName => "Python";

    public string GetStartingCode() => "def main(): \n" +
                                       "\tprint('Hello World!')\n" +
                                       "\n" +
                                       "if __name__ == '__main__':\n" +
                                       "\tmain()\n";
    
    public override string ToString() {
        return DisplayName;
    }

}
