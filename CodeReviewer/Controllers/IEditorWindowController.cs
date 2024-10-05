using CodeReviewer.Models.Languages;
using Wpf.Ui.Appearance;

namespace CodeReviewer.Controllers;

public interface IEditorWindowController {

    Task AddCustomLanguageToEditorAsync(IProgrammingLanguage programmingLanguage);
    Task CreateAsync();
    void DispatchScript(string script);
    Task<string> GetContent();
    Task SetContentAsync(string content);
    Task SetLanguageAsync(IProgrammingLanguage? programmingLanguage);
    Task SetThemeAsync(ApplicationTheme theme);

}
