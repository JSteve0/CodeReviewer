using OpenAI.Chat;

namespace CodeReviewer.Services.WebServices;

public static class OpenAIService {

    private const string Model = "gpt-4o";
    //TODO: Remove this before committing

    public static ChatClient ChatClient { get; private set; } = new (Model, OpenAIApiKey);

}
