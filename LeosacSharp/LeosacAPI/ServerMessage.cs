using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LeosacAPI;

public class ServerMessage
{
    [JsonPropertyName("status_code")]
    public APIStatusCode StatusCode { get; set; }
    
    [JsonPropertyName("status_string")]
    public string StatusString { get; set; }
    
    //std::string status_string;
    [JsonPropertyName("uuid")]
    public string uuid { get; set; }
    
    [JsonPropertyName("type")]
    public string type { get; set; }
    
    [JsonPropertyName("content")]
    public JsonObject content { get; set; }
}
