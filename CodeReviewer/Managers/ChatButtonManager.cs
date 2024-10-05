using Wpf.Ui.Controls;

namespace CodeReviewer.Managers;

public class ChatButtonManager(Button chatButton) {

    public void SetActiveAppearance() {
        chatButton.Appearance = ControlAppearance.Primary;
    }

    public void SetInactiveAppearance() {
        chatButton.Appearance = ControlAppearance.Secondary;
    }
}

