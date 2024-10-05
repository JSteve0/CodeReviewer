using Newtonsoft.Json;

namespace CodeReviewer.Models.Languages;

public class LanguageModel : IProgrammingLanguage {

    [JsonProperty]
    public required string Name { get; set; }

    [JsonProperty]
    public string? MonacoName { get; set; }
    [JsonProperty]
    public required List<string> Extensions { get; set; }
    
    [JsonProperty]
    public required string StartingCode { get; set; }
    
    public override string ToString() {
        return Name;
    }

}
