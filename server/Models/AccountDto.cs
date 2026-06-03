using System.Text.Json.Serialization;

namespace server.Models;

public class AccountDto
{
    [JsonPropertyName("puuid")]
    public string? Puuid { get; set; }
}