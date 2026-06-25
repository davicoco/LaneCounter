using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using server.Models;
namespace server.Services;

public class RiotApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public RiotApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string?> GetPuuidAsync(string gameName, string tagLine)
    {
        var url = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}";
        var response = await _httpClient.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        var account = JsonSerializer.Deserialize<AccountDto>(responseBody);
        if (account?.Puuid is null)
            return null;
        return account.Puuid;
    }

    public async Task<List<LeagueEntryDto>> GetLeagueEntriesAsync(string encryptedPUUID)
    {
        var url = $"https://euw1.api.riotgames.com/lol/league/v4/entries/by-puuid/{encryptedPUUID}";
        var response = await _httpClient.GetAsync(url);
        var responseBody = await response.Content.ReadAsStringAsync();
        var playerEntries = JsonSerializer.Deserialize<List<LeagueEntryDto>>(responseBody);
        if (playerEntries is null)
        {
            return [];
        }
        return playerEntries;

    }

    public async Task<List<string>> GetMatchIdsAsync(string puuid)
    {
        var url = $"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids";
        var response = await _httpClient.GetAsync(url);
        var responseBody = await response.Content.ReadAsStringAsync();
        var playerMatches = JsonSerializer.Deserialize<List<string>>(responseBody);
        if (playerMatches is null)
        {
            return [];
        }
        return playerMatches;
    }

    public async Task<MatchDto?> GetMatchInfoAsync(string matchId)
    {
        var url = $"https://europe.api.riotgames.com/lol/match/v5/matches/{matchId}";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var responseBody = await response.Content.ReadAsStringAsync();
        var match = JsonSerializer.Deserialize<MatchDto>(responseBody);
        return match;
    }

    public async Task<List<MatchDto>> GetMatchesInfoAsync(List<string> matchIds)
    {
        var semaphore = new SemaphoreSlim(2);

        async Task<MatchDto?> FetchWithLimit(string matchId)
        {
            await semaphore.WaitAsync();
            try
            {
                return await GetMatchInfoAsync(matchId);
            }
            finally
            {
                semaphore.Release();
            }
        }

        var pendingMatches = new List<Task<MatchDto?>>();

        foreach( var matchId in matchIds)
        {
            Task<MatchDto?> pendingMatch = FetchWithLimit(matchId);
            pendingMatches.Add(pendingMatch);
        }
        MatchDto?[] results = await Task.WhenAll(pendingMatches);

        var matches = new List<MatchDto>();

        foreach(var match in results)
        {
            if (match == null)
            {
                continue;
            }
            matches.Add(match);
        }
        Console.WriteLine($"matchIds: {matchIds.Count}, matches: {matches.Count}");
        return matches;
    }


}