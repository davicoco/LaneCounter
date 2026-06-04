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
        var apiKey = _configuration["RIOT_API_KEY"];
        _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
        var response = await _httpClient.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        var account = JsonSerializer.Deserialize<AccountDto>(responseBody);
        if(account?.Puuid is null)
        throw new Exception("No value for account-puuid was found");
        return account.Puuid;
    }
}