using CodeReviewer.Models.Languages;
using Wpf.Ui.Appearance;

namespace CodeReviewer.Controllers;

public interface IEditorWindowController {

    Task CreateAsync();
    Task SetThemeAsync(ApplicationTheme theme);
    Task SetLanguageAsync(IProgrammingLanguage? programmingLanguage);
    Task SetContentAsync(string content);
    Task<string> GetContent();
    void DispatchScript(string script);

}
