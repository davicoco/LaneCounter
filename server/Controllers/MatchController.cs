using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
    private readonly RiotApiClient _riotApiClient;

    public MatchController(RiotApiClient riotApiClient)
    {
        _riotApiClient = riotApiClient;
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
        return Ok(matchIds);
    }
}