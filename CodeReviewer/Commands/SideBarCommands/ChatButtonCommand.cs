using CodeReviewer.Managers;

namespace CodeReviewer.Commands.SideBarCommands;

public class ChatButtonCommand(ChatPanelManager chatPanelManager, ChatButtonManager chatButtonManager)
    : CommandBase {

    public override void Execute(object? parameter) {
        Logger.LogInfo("Toggling chat window");

        if (chatPanelManager.IsChatWindowVisible) {
            chatPanelManager.HideChatWindow();
            chatButtonManager.SetInactiveAppearance();
        }
        else {
            chatPanelManager.ShowChatWindow();
            chatButtonManager.SetActiveAppearance();
        }
    }
}

