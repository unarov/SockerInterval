namespace ProbabilityService;

using Interfaces;

public class ProbabilityProvider : IProbabilityProvider
{
    private static int MATCH_TIME = 90;
    private IStatistic matchStat;
    public ProbabilityProvider(IStatistic matchStatistic)
    {
        this.matchStat = matchStatistic;
    }
    public double MatchScore((int,int) matchScore)
    {
        return matchStat.ScorePercentage(matchScore);
    }
    public double MatchScoreIfCurrentMatchScore((int,int) matchScore, (int,int) currentMatchScore, int time)
    {
        var posibleScores = matchStat.GetPosibleOutcomes();

        var currentMatchScoreFull = posibleScores.Sum(posibleScore=>{
            return matchStat.ScorePercentage(posibleScore) * CurrentMatchScoreIfFinalScore(currentMatchScore, time, posibleScore);
        });
        return CurrentMatchScoreIfFinalScore(currentMatchScore, time, matchScore) * matchStat.ScorePercentage(matchScore) / currentMatchScoreFull;
    }
    public double CurrentMatchScoreIfFinalScore((int,int) currentMatchScore, int time, (int,int) matchScore)
    {
        if (time<0 || time>MATCH_TIME)
            throw new  Exception($"Time should be between 0 and {MATCH_TIME}");
        var timeProportion = time * 1.0 / MATCH_TIME;

        if (currentMatchScore.Item1>matchScore.Item1 || currentMatchScore.Item2>matchScore.Item2)
            return 0;
        
        var teamOneProbability = BernoulliTrial(timeProportion, currentMatchScore.Item1, matchScore.Item1);
        var teamTwoProbability = BernoulliTrial(timeProportion, currentMatchScore.Item2, matchScore.Item2);

        return teamOneProbability*teamTwoProbability;
    }

    public double TeamScore(Team team, int score)
    {
        return matchStat.TeamScorePercentage(team, score);
    }
    private double BernoulliTrial(double p, int positive, int total)
    {
        var combinations = Combinations(total, positive);
        var pow1 = Math.Pow(p, positive);
        var pow2 = Math.Pow((1.0-p),(total-positive));
        return combinations * pow1 * pow2;
    }
    private static long Combinations(int n, int r)
    {
        // naive: return Factorial(n) / (Factorial(r) * Factorial(n - r));
        return Permutations(n, r) / Factorial(r);
    }

    private static long Permutations(int n, int r)
    {
        // naive: return Factorial(n) / Factorial(n - r);
        return FactorialDivision(n, n - r);
    }

    private static long FactorialDivision(int topFactorial, int divisorFactorial)
    {
        long result = 1;
        for (int i = topFactorial; i > divisorFactorial; i--)
            result *= i;
        return result;
    }

    private static long Factorial(int i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }
}