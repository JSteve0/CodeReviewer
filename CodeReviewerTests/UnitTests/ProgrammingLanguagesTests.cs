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
        IProgrammingLanguage? response = ProgrammingLanguages.GetProgrammingLanguageFromExtension("java");
        Assert.Null(response);
    }
}