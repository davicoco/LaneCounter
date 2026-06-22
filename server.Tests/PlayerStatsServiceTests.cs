using server.Models;
using server.Services;

namespace server.Tests;

public class PlayerStatsServiceTests
{
    private readonly string puuid = "test-puuid";
    private readonly PlayerStatsService service = new PlayerStatsService();

    [Fact]
    public void CalculatePlayerStats_ZeroMatchesPlayed_ReturnDtoWithNulledFields()
    {
        var matches = new List<MatchDto> ();

        var result = service.CalculatePlayerStats(matches, puuid);
        
        Assert.Null(result.RecentWinRate);
        Assert.Null(result.AverageKills);
        Assert.Null(result.AverageDeaths);
        Assert.Null(result.AverageAssists);

    }

    [Fact]
    public void CalculatePlayerStats_FiveWinsOutOfTen_WinRateIsZeroPointFive()
    {

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

        Assert.Equal(0.5, result.RecentWinRate);
    }

    [Fact]
    public void CalculatePlayerStats_TwoMatchesWithDifferentKDA_ReturnsAverageKDA()
    {
        var matches = new List<MatchDto>
        {
            CreateMatch(puuid , true, 9, 7, 5),
            CreateMatch(puuid, true, 4, 4, 4)
        };

        var result = service.CalculatePlayerStats(matches, puuid);

        Assert.Equal(6.5, result.AverageKills!.Value, 2);
        Assert.Equal(5.5, result.AverageDeaths!.Value, 2);
        Assert.Equal(4.5, result.AverageAssists!.Value, 2);
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
