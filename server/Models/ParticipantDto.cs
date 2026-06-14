using System.Text.Json.Serialization;

namespace server.Models;

public class ParticipantDto
{
    [JsonPropertyName("puuid")]
    public string? Puuid { get; set; }
    [JsonPropertyName("championName")]
    public string? ChampionName { get; set; }
    [JsonPropertyName("kills")]
    public int? Kills { get; set; }
    [JsonPropertyName("deaths")]
    public int? Deaths { get; set; }
    [JsonPropertyName("assists")]
    public int? Assists { get; set; }
    [JsonPropertyName("win")]
    public bool? Win { get; set; }
}