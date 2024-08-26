using CodeReviewer.Models;

namespace CodeReviewerTests.UnitTests;

public class ProgrammingLanguagesTests {

    [Fact]
    public void GetExtension_ShouldReturnCorrectExtension () {
        var jsResponse = ProgrammingLanguages.GetExtension(ProgrammingLanguagesEnum.JavaScript);
        Assert.Equal("js", jsResponse);
        
        var csResponse = ProgrammingLanguages.GetExtension(ProgrammingLanguagesEnum.CSharp);
        Assert.Equal("cs", csResponse);
    }

    [Fact]
    public void GetProgrammingLanguageFromExtension_ShouldReturnCorrectProgrammingLanguage() {
        var jsResponse = ProgrammingLanguages.GetProgrammingLanguageFromExtension("js");
        Assert.Equal(ProgrammingLanguagesEnum.JavaScript, jsResponse);
        
        jsResponse = ProgrammingLanguages.GetProgrammingLanguageFromExtension("JS");
        Assert.Equal(ProgrammingLanguagesEnum.JavaScript, jsResponse);
        
        var csResponse = ProgrammingLanguages.GetProgrammingLanguageFromExtension("cs");
        Assert.Equal(ProgrammingLanguagesEnum.CSharp, csResponse);
        
        csResponse = ProgrammingLanguages.GetProgrammingLanguageFromExtension("CS");
        Assert.Equal(ProgrammingLanguagesEnum.CSharp, csResponse);
    }
    
    [Fact]
    public void GetProgrammingLanguageFromExtension_BadInputShouldReturnNull() {
        var response = ProgrammingLanguages.GetProgrammingLanguageFromExtension("java");
        Assert.Null(response);
    }
    
}
