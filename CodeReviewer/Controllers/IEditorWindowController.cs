using CodeReviewer.Models;
using Wpf.Ui.Appearance;

namespace CodeReviewer.Controllers;

public interface IEditorWindowController {
    Task CreateAsync();
    Task SetThemeAsync(ApplicationTheme theme);
    Task SetLanguageAsync(ProgrammingLanguagesEnum? programmingLanguage);
    Task SetContentAsync(string content);
    Task<string> GetContent();
    void DispatchScript(string script);
}
