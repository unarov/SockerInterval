namespace MatchMakerService;

using System.Collections.Generic;
using Interfaces.Models;
using Interfaces.Services;
using System.Linq;
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
    public (int,int) GenerateScore((int,int) minimumScore)
    {
        var matchesCountDict = _matchStat.GetMatchesCount().Where(x=>x.Key.Item1>=minimumScore.Item1&&x.Key.Item2>=minimumScore.Item2);
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
    public List<Goal> GenerateGoalSequence()
    {
        var score = GenerateScore();
        List<Goal> goalSequence = new List<Goal>();
        for (int i=0; i<score.Item1; i++)
            goalSequence.Add(new Goal(){team=Team.HomeTeam,time=_random.Next(0,90)});
        for (int i=0; i<score.Item2; i++)
            goalSequence.Add(new Goal(){team=Team.GuestTeam,time=_random.Next(0,90)});

        goalSequence = goalSequence.OrderBy(x=>x.time).ToList();

        return goalSequence;
    }    
    public List<Team> GenerateGoalTeamSequence(List<Team> currentSequance, int time)
    {
        var currentScore = (currentSequance.Count(x=>x==Team.HomeTeam),currentSequance.Count(x=>x==Team.GuestTeam));
        var finalScore = GenerateScore(currentScore);
        List<Team> goalSequence = new List<Team>();
        goalSequence.AddRange(Enumerable.Repeat(Team.HomeTeam,finalScore.Item1-currentScore.Item1));
        goalSequence.AddRange(Enumerable.Repeat(Team.GuestTeam,finalScore.Item2-currentScore.Item2));
        goalSequence.OrderBy(x=>_random.NextDouble());
        var resultSequance = new List<Team>(currentSequance);
        resultSequance.AddRange(goalSequence);

        return resultSequance;
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
}
