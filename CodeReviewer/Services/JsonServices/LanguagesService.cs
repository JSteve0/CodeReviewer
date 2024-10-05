using System.IO;
using CodeReviewer.Logging;
using CodeReviewer.Models.Languages;
using Newtonsoft.Json;

namespace CodeReviewer.Services.JsonServices;

public class LanguagesService {

    private const string FilePath = "Languages.json";
    
    public List<LanguageModel> Languages;

    // Private constructor to prevent instantiation
    private LanguagesService() {
        Languages = LoadLanguages();
    }

    // Public property to access the singleton instance
    public static LanguagesService Instance { get; } = new();

    // Method to load the entire project details
    private List<LanguageModel> LoadLanguages() {
        string json = File.ReadAllText(FilePath);
        Languages = JsonConvert.DeserializeObject<LanguageCollection>(json)?.Languages ??
                    throw new InvalidOperationException("Failed to load languages");
    
        Logger.Instance.LogVerbose($"Loaded JSON File {FilePath} with data:\n" + Languages);
        return Languages;
    }

}
