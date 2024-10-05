using Newtonsoft.Json;

namespace CodeReviewer.Models;

public record AuthorsModel {

    [JsonProperty]
    public required string Name { get; set; }
    [JsonProperty]
    public string Email { get; private set; } = "";
    
    public AuthorsModel(string name, string email) {
        Name = name;
        Email = email;
    }

}
