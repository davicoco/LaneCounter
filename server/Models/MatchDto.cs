using System.Text.Json.Serialization;

namespace server.Models;

public class MatchDto
{
    [JsonPropertyName("info")]
    public required InfoDto Info { get; set; }
}