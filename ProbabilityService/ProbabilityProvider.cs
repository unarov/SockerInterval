namespace ProbabilityService;

using Interfaces;

public class ProbabilityProvider : IProbabilityProvider
{
    private MatchStat matchStat;
    public ProbabilityProvider()
    {
        this.matchStat = MatchStat.GetMatchStat();
    }
    public double ProbabilityMatchScore(MatchScore matchScore)
    {
        return matchStat.ScorePercentage(matchScore);
    }
    public double ProbabilityTeamScore(TeamScore teamScore)
    {
        return matchStat.TeamScorePercentage(teamScore);
    }
}