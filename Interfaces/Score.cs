namespace Interfaces;
public class TeamScore
{
    public Team team {get;}
    public int score {get;}
    public TeamScore(Team  team, int score)
    {
        this.team = team;
        this.score = score;
    }

}
public class MatchScore
{

    TeamScore[] teamScore;
    public MatchScore(Team teamOne, Team teamTwo, int scoreTeamOne, int scoreTeamTwo)
    {
        teamScore = new TeamScore[]{
            new TeamScore(teamOne, scoreTeamOne),
            new TeamScore(teamTwo, scoreTeamTwo)
            };
    }
    public int GetTeamScore(Team team)
    {
        var targetTeamScore = teamScore.FirstOrDefault(x=>x.team==team);
        if (targetTeamScore == null) 
            throw new Exception($"The team {team} did not participate in the match");
        return targetTeamScore.score;
    }
    public override string ToString()
    {
        return  $"({teamScore[0].score}, {teamScore[1].score})";
    }
}