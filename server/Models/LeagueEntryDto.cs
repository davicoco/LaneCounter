using System.Text.Json.Serialization;

namespace server.Models;

public class LeagueEntryDto
{
    [JsonPropertyName("queueType")]
    public string? QueueType { get; set; }

    [JsonPropertyName("tier")]
    public string? Tier { get; set; }

    [JsonPropertyName("rank")]
    public string? Rank { get; set; }

    [JsonPropertyName("leaguePoints")]
    public int? LeaguePoints { get; set; }

    [JsonPropertyName("wins")]
    public int? Wins { get; set; }

    [JsonPropertyName("losses")]
    public int? Losses { get; set; }

    [JsonPropertyName("hotStreak")]
    public bool? HotStreak { get; set; }

    [JsonPropertyName("veteran")]
    public bool? Veteran { get; set; }

    [JsonPropertyName("freshBlood")]
    public bool? FreshBlood { get; set; }
    
    [JsonPropertyName("inactive")]
    public bool? Inactive { get; set; }

}