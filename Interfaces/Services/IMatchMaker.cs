namespace Interfaces.Services;
public interface IMatchMaker
{
    public (int,int) GenerateScore();
    public (int, int) GenerateScoreAtTime(int time, (int,int) finalScore);
    public (int,int) GenerateScoreAtTime(int time);
}