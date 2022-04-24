namespace MatchMakerService;
using Interfaces.Services;
public class MatchMaker : IMatchMaker
{
    private Random _random;
    private IStatistic _matchStat;
    int MATCH_TIME = 90;
    public MatchMaker(IStatistic statistic)
    {
        _random = new Random();
        _matchStat = statistic;
    }
    public MatchMaker(IStatistic statistic, int seed)
    {
        _random = new Random(seed);
        _matchStat = statistic;
    }
    public (int,int) GenerateScore()
    {
        var matchesCountDict = _matchStat.GetMatchesCount();
        var totalMatches = matchesCountDict.Sum(x=>x.Value);
        
        int generatedMatchScoreIndex = _random.Next(0,totalMatches);
        int index = 0;
        foreach (var matchStat in matchesCountDict){
            if (matchStat.Value + index > generatedMatchScoreIndex){
                return matchStat.Key;
            }
            index += matchStat.Value;
        }
        throw new Exception("Generator ERROR");
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
