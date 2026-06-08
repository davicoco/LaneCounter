using System.Text.Json.Serialization;

namespace server.Models;

public class InfoDto
{
    [JsonPropertyName("gameMode")]
    public string? GameMode { get; set; }
    [JsonPropertyName("gameCreation")]
    public long? GameCreation { get; set; }
    [JsonPropertyName("gameDuration")]
    public int? GameDuration { get; set; }
    [JsonPropertyName("queueId")]
    public int? QueueId {get; set;}
    [JsonPropertyName("participants")]
    public List<ParticipantDto>? Participants {get; set;}
}