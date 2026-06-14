using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly RiotApiClient _riotApiClient;

    public AccountController(RiotApiClient riotApiClient)
    {
        _riotApiClient = riotApiClient;
    }

    [HttpGet("{gameName}/{tagLine}")]
    public async Task<IActionResult> GetPuuidAsync(string gameName, string tagLine)
    {
        var puuid = await _riotApiClient.GetPuuidAsync(gameName,tagLine);
        if(puuid is null)
        {
            return NotFound();
        }
        return Ok(new {puuid = puuid});
    }
}