using System.Text.Json.Serialization;

namespace server.Models;

public class MatchDto
{
    [JsonPropertyName("info")]
    public InfoDto? Info { get; set; }
}