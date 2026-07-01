using server.Models;

namespace server.Services;

public class PlayerStatsService
{
    public PlayerStatsDto CalculatePlayerStats(List<MatchDto> matches, string puuid)
    {
        if (matches.Count == 0)
        {
            return new PlayerStatsDto();
        }

        double totalKills = 0;
        double totalDeaths = 0;
        double totalAssists = 0;
        double wins = 0;
        int remakes = 0;
        foreach (var match in matches)
        {   
            var player = match.Info.Participants.First((p) => p.Puuid == puuid);
            if (player.GameEndedInEarlySurrender)
            {
                remakes++;
                continue;
            }
            totalKills += player.Kills;
            totalDeaths += player.Deaths;
            totalAssists += player.Assists;
            if (player.Win)
            {
                wins++;
            }

        }

        int playedMatches = matches.Count - remakes;

        if (playedMatches == 0)
        {
            return new PlayerStatsDto();
        }

        var stats = new PlayerStatsDto
        {
            AverageKills = totalKills / playedMatches,
            AverageDeaths = totalDeaths / playedMatches,
            AverageAssists = totalAssists / playedMatches,
            RecentWinRate = wins / playedMatches
        };

        return stats;
    }
}