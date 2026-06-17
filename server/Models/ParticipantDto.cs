using System.Text.Json.Serialization;

namespace server.Models;

public class ParticipantDto
{
    [JsonPropertyName("puuid")]
    public required string Puuid { get; set; }
    [JsonPropertyName("championName")]
    public required string ChampionName { get; set; }
    [JsonPropertyName("kills")]
    public required int Kills { get; set; }
    [JsonPropertyName("deaths")]
    public required int Deaths { get; set; }
    [JsonPropertyName("assists")]
    public required int Assists { get; set; }
    [JsonPropertyName("win")]
    public required bool Win { get; set; }
}