using System.Text.Json.Serialization;

namespace server.Models;

public class InfoDto
{
    [JsonPropertyName("gameMode")]
    public required string GameMode { get; set; }

    [JsonPropertyName("gameCreation")]
    public required long GameCreation { get; set; }

    [JsonPropertyName("gameDuration")]
    public required int GameDuration { get; set; }

    [JsonPropertyName("queueId")]
    public required int QueueId {get; set;}
    
    [JsonPropertyName("participants")]
    public required List<ParticipantDto> Participants {get; set;}
}