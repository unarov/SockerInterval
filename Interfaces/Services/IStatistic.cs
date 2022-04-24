namespace Interfaces.Services;
using Interfaces.Models;
public interface IStatistic
{
    double TeamScorePercentage(Team team, int score);
    IEnumerable<(int,int)> GetPosibleOutcomes();
    double ScorePercentage((int, int) matchScore);
}