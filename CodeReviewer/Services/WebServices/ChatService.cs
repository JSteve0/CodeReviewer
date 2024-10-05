using CodeReviewer.Logging;
using OpenAI.Chat;

namespace CodeReviewer.Services.WebServices;

public static class ChatService {

    public static async Task<ChatCompletion?> SendChatRequestAsync(string chatText) {
        if (string.IsNullOrWhiteSpace(chatText)) return null;

        try {
            ChatCompletion results = await OpenAIService.ChatClient.CompleteChatAsync(chatText);

            return results;
        }
        catch (Exception ex) {
            Logger.Instance.LogError($"Error during chat request: {ex.Message}\n{ex.StackTrace}");
        }

        return null;
    }

}
