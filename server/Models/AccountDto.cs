using System.Text.Json.Serialization;

namespace server.Models;

public class AccountDto
{
    [JsonPropertyName("puuid")]
    public required string Puuid { get; set; }
}