namespace Interfaces;
public struct TeamScore
{
    public Team team {get;}
    public int score {get;}
    public TeamScore(Team  team, int score)
    {
        this.team = team;
        this.score = score;
    }
    public override bool Equals(object? obj)  => obj is TeamScore other && this.Equals(other);
    public bool Equals(TeamScore otherTeamScore) => team == otherTeamScore.team && score == otherTeamScore.score;
    public override int GetHashCode() => (team, score).GetHashCode();
    public static bool operator ==(TeamScore lhs, TeamScore rhs) => lhs.Equals(rhs);
    public static bool operator !=(TeamScore lhs, TeamScore rhs) => !(lhs == rhs);
    
}
public struct MatchScore
{
    TeamScore teamOneScore;
    TeamScore teamTwoScore;
    public MatchScore(Team teamOne, Team teamTwo, int scoreTeamOne, int scoreTeamTwo)
    {
        if (teamOne==teamTwo) throw new Exception("Should be different teams");
        teamOneScore = new TeamScore(teamOne, scoreTeamOne);
        teamTwoScore = new TeamScore(teamTwo, scoreTeamTwo);
    }
    public int GetTeamScore(Team team)
    {
        if (teamOneScore.team==team) return teamOneScore.score;
        if (teamTwoScore.team==team) return teamTwoScore.score;
        throw new Exception($"The team {team} did not participate in the match");
    }
    public override string ToString()
    {
        return  $"({teamOneScore.score}, {teamTwoScore.score})";
    }
    public override bool Equals(object? obj)  => obj is MatchScore other && this.Equals(other);
    public bool Equals(MatchScore otherMatchScore) => teamOneScore==otherMatchScore.teamOneScore&&teamTwoScore==otherMatchScore.teamTwoScore
        || teamOneScore==otherMatchScore.teamTwoScore&&teamTwoScore==otherMatchScore.teamOneScore;
    public override int GetHashCode() => (teamOneScore,teamTwoScore).GetHashCode();
    public static bool operator ==(MatchScore lhs, MatchScore rhs) => lhs.Equals(rhs);
    public static bool operator !=(MatchScore lhs, MatchScore rhs) => !(lhs == rhs);
}