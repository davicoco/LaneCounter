using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
    private readonly RiotApiClient _riotApiClient;
    private readonly PlayerStatsService _playerStatsService;

    public MatchController(RiotApiClient riotApiClient, PlayerStatsService playerStatsService)
    {
        _riotApiClient = riotApiClient;
        _playerStatsService = playerStatsService;
    }

    [HttpGet("{gameName}/{tagLine}")]
    public async Task<IActionResult> GetMatchesAsync(string gameName, string tagLine)
    {
        var puuid = await _riotApiClient.GetPuuidAsync(gameName,tagLine);
        if(puuid is null)
        {
            return NotFound();
        }
        var matchIds = await _riotApiClient.GetMatchIdsAsync(puuid);

        var matches = await _riotApiClient.GetMatchesInfoAsync(matchIds);

        var stats = _playerStatsService.CalculatePlayerStats(matches,puuid);

        var response = new MatchHistoryResponseDto
        {
            Matches = matches,
            PlayerStats = stats
        };

        return Ok(response);    
        
    }
}