using System.Text.Json.Serialization;

namespace LeosacAPI;

public class ClientMessage
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("uuid")] public string Uuid { get; set; } = Guid.NewGuid().ToString();
    
    [JsonPropertyName("content")]
    public object Content { get; set; } = new{};
}
