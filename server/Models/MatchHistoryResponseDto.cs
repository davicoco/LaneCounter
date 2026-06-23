namespace server.Models;

public class MatchHistoryResponseDto
{
    public required List<MatchDto> Matches {get; set;}
    public required PlayerStatsDto PlayerStats {get; set;}
}