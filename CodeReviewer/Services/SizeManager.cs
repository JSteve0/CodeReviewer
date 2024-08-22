using System.Windows;
using System.Windows.Controls;
using CodeReviewer.Controls;

namespace CodeReviewer.Services;

public class SizeUpdater(Grid mainGrid, Panel actionsPanel, double panelWidth) {

    public void UpdateSizes(double actualWidth, double actualHeight)
    {
        double editorWidth = Math.Max(actualWidth - panelWidth, 0.0);
        mainGrid.ColumnDefinitions[0].Width = new GridLength(editorWidth);
        mainGrid.ColumnDefinitions[1].Width = new GridLength(panelWidth);

        actionsPanel.Width = panelWidth;
        mainGrid.RowDefinitions[0].Height = new GridLength(actualHeight);

        foreach (TextEditorControl editorControl in mainGrid.Children.OfType<TextEditorControl>())
        {
            editorControl.WebView.Height = actualHeight;
        }
    }
}
