using System.Runtime.CompilerServices;
using System.Text.Json;
using server.Models;
namespace server.Services;

public class RiotApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public RiotApiClient(HttpClient httpClient,IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GetPuuidAsync(string gameName, string tagLine)
    {
        var url = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}";
        var response = await _httpClient.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        var account = JsonSerializer.Deserialize<AccountDto>(responseBody);
        if(account?.Puuid is null)
        throw new Exception("No value for account-puuid was found");
        return account.Puuid;
    }

    public async Task<List<LeagueEntryDto>> GetLeagueEntriesAsync(string encryptedPUUID)
    {
        var url = $"https://euw1.api.riotgames.com/lol/league/v4/entries/by-puuid/{encryptedPUUID}";
        var response = await _httpClient.GetAsync(url);
        var responseBody = await response.Content.ReadAsStringAsync();
        var playerEntries = JsonSerializer.Deserialize<List<LeagueEntryDto>>(responseBody);
        if(playerEntries is null)
        {
            return [];
        }
        return playerEntries;

    }
}