namespace MatchMakerService;
using Interfaces.Services;
public class MatchMaker : IMatchMaker
{
    private Random _random;
    private IStatistic _matchStat;
    int MATCH_TIME = 90;
    public MatchMaker(IStatistic matchStat)
    {
        _random = new Random();
        _matchStat = matchStat;
    }
    public MatchMaker(IStatistic matchStat, int seed)
    {
        _random = new Random(seed);
        _matchStat = matchStat;
    }
    public (int,int) GenerateScore()
    {
        throw new NotImplementedException();
    }
    public (int, int) GenerateScoreAtTime(int time, (int,int) finalScore)
    {
        double matchProportion = time * 1.0 / MATCH_TIME;
        var score = (0,0);
        for (int i=0; i< finalScore.Item1; i++){
            var goalTime = _random.NextDouble();
            if (goalTime<matchProportion) score.Item1++;
        }
        for (int i=0; i< finalScore.Item2; i++){
            var goalTime = _random.NextDouble();
            if (goalTime<matchProportion) score.Item2++;
        }
        return score;
    }
    public (int,int) GenerateScoreAtTime(int time)
    {
        var finalScore = GenerateScore();
        return GenerateScoreAtTime(time, finalScore);
    }
}
