using server.Models;
using server.Services;

namespace server.Tests;

public class PlayerStatsServiceTests
{

    [Fact]
    public void CalculatePlayerStats_FiveWinsOutOfTen_WinRateIsZeroPointFive()
    {
         var service = new PlayerStatsService();

         var puuid = "test-puuid";

        var matches = new List<MatchDto>
        {
            CreateMatch(puuid, true, 5, 5, 5),
            CreateMatch(puuid, true, 5, 5, 5),
            CreateMatch(puuid, true, 5, 5, 5),
            CreateMatch(puuid, true, 5, 5, 5),
            CreateMatch(puuid, true, 5, 5, 5),
            CreateMatch(puuid, false, 5, 5, 5),
            CreateMatch(puuid, false, 5, 5, 5),
            CreateMatch(puuid, false, 5, 5, 5),
            CreateMatch(puuid, false, 5, 5, 5),
            CreateMatch(puuid, false, 5, 5, 5),

        };

        var result = service.CalculatePlayerStats(matches, puuid);

        Assert.Equal(0.5,result.RecentWinRate);
    }

    private MatchDto CreateMatch(string puuid, bool win, int kills, int deaths, int assists)
    {
        return new MatchDto
        {
            Info = new InfoDto
            {
                GameMode = "CLASSIC",
                GameCreation = 2,
                GameDuration = 2,
                QueueId = 420,
                Participants = new List<ParticipantDto>
                {
                    new ParticipantDto
                    {
                        Puuid = puuid,
                        ChampionName = "Garen",
                        Kills = kills,
                        Deaths = deaths,
                        Assists = assists,
                        Win = win   
                    }
                }
            }
        };
    }
}
