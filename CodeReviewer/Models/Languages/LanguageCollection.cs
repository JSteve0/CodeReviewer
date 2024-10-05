using Newtonsoft.Json;

namespace CodeReviewer.Models.Languages;

public class LanguageCollection {

    [JsonProperty]
    public List<LanguageModel> Languages { get; set; } = [];

}
