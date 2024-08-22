using System.Collections.Generic;
using System.Windows.Controls;
using CodeReviewer.Controls;

namespace CodeReviewer.Services;

public class TabManager(TabControl tabControl) {
    private readonly Dictionary<string, TextEditorControl> _editorControls = new();

    public TextEditorControl AddNewTab(string tabTitle)
    {
        var editorControl = new TextEditorControl();
        TabItem tabItem = CreateNewTabItem(tabTitle, editorControl);

        editorControl.UpdateLayout();
        _editorControls.Add(tabTitle, editorControl);
        tabControl.Items.Add(tabItem);

        return editorControl;
    }

    private TabItem CreateNewTabItem(string tabTitle, TextEditorControl editorControl) => new()
    {
        Content = editorControl,
        Header = tabTitle
    };

    public string GenerateUniqueTabName(string baseName)
    {
        string newName = baseName;
        int count = 1;
        while (_editorControls.ContainsKey(newName))
        {
            string[] splitString = newName.Split(".");
            string fileName = splitString[0];
            string fileExtension = splitString[1];
            
            newName = $"{fileName} ({count++}).{fileExtension}";
        }

        return newName;
    }

    public IEnumerable<TextEditorControl> GetAllEditorControls() => _editorControls.Values;

    public TextEditorControl? GetEditorControl(string tabTitle) => _editorControls.GetValueOrDefault(tabTitle);
}
