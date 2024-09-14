using CodeReviewer.Models.Languages;

namespace CodeReviewerTests.UnitTests;

public class ProgrammingLanguagesTests {
    [Fact]
    public void GetProgrammingLanguageFromExtension_ShouldReturnCorrectProgrammingLanguage() {
        var javaScriptString = new JavaScriptProgrammingLanguage().ToString();
        var cSharpString = new CSharpProgrammingLanguage().ToString();

        IProgrammingLanguage? jsResponse = ProgrammingLanguages.GetProgrammingLanguageFromExtension("js");
        Assert.Equal(javaScriptString, jsResponse?.ToString());

        jsResponse = ProgrammingLanguages.GetProgrammingLanguageFromExtension("JS");
        Assert.Equal(javaScriptString, jsResponse?.ToString());

        IProgrammingLanguage? csResponse = ProgrammingLanguages.GetProgrammingLanguageFromExtension("cs");
        Assert.Equal(cSharpString, csResponse?.ToString());

        csResponse = ProgrammingLanguages.GetProgrammingLanguageFromExtension("CS");
        Assert.Equal(cSharpString, csResponse?.ToString());
    }

    [Fact]
    public void GetProgrammingLanguageFromExtension_BadInputShouldReturnNull() {
        IProgrammingLanguage? response = ProgrammingLanguages.GetProgrammingLanguageFromExtension("c++");
        Assert.Null(response);
    }

    public static IEnumerable<object[]> LanguageTestData => new List<object[]> {
        new object[] { new CSharpProgrammingLanguage() },
        new object[] { new JavaProgrammingLanguage() },
        new object[] { new JavaScriptProgrammingLanguage() },
        new object[] { new TypeScriptProgrammingLanguage() }
    };

    [Theory]
    [MemberData(nameof(LanguageTestData))]
    public void GetProgrammingLanguages_ShouldContainLanguage(IProgrammingLanguage language) {
        Assert.Equal(ProgrammingLanguages.GetAllLanguages().Count(), LanguageTestData.Count());
        Assert.Contains(
            ProgrammingLanguages.GetAllLanguages(),
            programmingLanguage => programmingLanguage.ToString().Equals(language.MonacoName, StringComparison.CurrentCultureIgnoreCase));
    }

    public static IEnumerable<object[]> LanguageTestDataWithStarterCode => new List<object[]> {
        new object[] { new CSharpProgrammingLanguage(), "public static void Main(String[] args) {" },
        new object[] { new JavaProgrammingLanguage(), "public static void main(String[] args) {" },
        new object[] { new JavaScriptProgrammingLanguage(), "console.log('Hello World!');" },
        new object[] { new TypeScriptProgrammingLanguage(), "function helloWorld() {"}
    };

    [Theory]
    [MemberData(nameof(LanguageTestDataWithStarterCode))]
    public void GetProgrammingLanguages_ShouldContainRelevantStarterCode(IProgrammingLanguage language, string text) {
        Assert.Equal(ProgrammingLanguages.GetAllLanguages().Count(), LanguageTestDataWithStarterCode.Count());
        Assert.Contains(
            ProgrammingLanguages.GetAllLanguages(),
            programmingLanguage => 
                programmingLanguage.ToString().Equals(language.MonacoName, StringComparison.CurrentCultureIgnoreCase)
                && programmingLanguage.GetStartingCode().Contains(text));
    }

}
