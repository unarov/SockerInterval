using Interfaces.Models;
namespace Interfaces.Services;
public interface IMatchMaker
{
    public (int,int) GenerateScore();
    public List<Goal> GenerateGoalSequence();
    public (int, int) GenerateScoreAtTime(int time, (int,int) finalScore);
}