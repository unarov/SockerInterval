namespace Interfaces;
public interface IStatistic
{
    double TeamScorePercentage(Team team, int score);
    IEnumerable<(int,int)> GetPosibleOutcomes();
    double ScorePercentage((int, int) matchScore);
}