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
        foreach (var match in matches)
        {
            var player = match.Info.Participants.First((p) => p.Puuid == puuid);
            totalKills += player.Kills;
            totalDeaths += player.Deaths;
            totalAssists += player.Assists;
            if (player.Win)
            {
                wins++;
            }

        }
        var stats = new PlayerStatsDto
        {
            AverageKills = totalKills / matches.Count,
            AverageDeaths = totalDeaths / matches.Count,
            AverageAssists = totalAssists / matches.Count,
            RecentWinRate = wins / matches.Count
        };

        return stats;
    }
}