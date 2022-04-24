namespace MatchMakerService;
public class MatchMaker
{
    Random random;
    int MATCH_TIME = 90;
    public MatchMaker()
    {
        random = new Random();
    }
    public MatchMaker(int seed)
    {
        random = new Random(seed);
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
            var goalTime = random.NextDouble();
            if (goalTime<matchProportion) score.Item1++;
        }
        for (int i=0; i< finalScore.Item2; i++){
            var goalTime = random.NextDouble();
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
