namespace Interfaces;
public interface IProbabilityProvider
{
    public double MatchScore((int,int)matchScore);
    public double TeamScore(Team team, int score);
    public double MatchScoreIfCurrentMatchScore((int,int) matchScore, (int,int) currentMatchScore, int time);
}