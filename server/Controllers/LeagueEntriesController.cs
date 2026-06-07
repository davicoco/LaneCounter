using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeagueEntriesController : ControllerBase
{
    private readonly RiotApiClient _riotApiClient;

    public LeagueEntriesController(RiotApiClient riotApiClient)
    {
        _riotApiClient = riotApiClient;
    }

    [HttpGet("{gameName}/{tagLine}")]
    public async Task<IActionResult> GetLeagueEntries(string gameName, string tagLine)
    {
        var puuid = await _riotApiClient.GetPuuidAsync(gameName,tagLine);
        if(puuid is null)
        {
            return NotFound();
        }
        var entries = await _riotApiClient.GetLeagueEntriesAsync(puuid);
        return Ok(entries);
    }
}