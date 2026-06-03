using System.Text.Json;
using server.Models;
namespace server.Services;

public class RiotApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    public RiotApiClient(IHttpClientFactory httpClientFactory,IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<string> GetPuuidAsync(string gameName, string tagLine)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var url = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}";
        var apiKey = _configuration["RIOT_API_KEY"];
        httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
        var response = await httpClient.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        var account = JsonSerializer.Deserialize<AccountDto>(responseBody);
        if(account?.Puuid is null)
        throw new Exception("No value for account-puuid was found");
        return account.Puuid;
    }
}