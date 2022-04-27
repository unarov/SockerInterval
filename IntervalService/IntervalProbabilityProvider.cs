using System.Collections.Generic;
using Interfaces.Models;
using Interfaces.Services;

namespace IntervalService;
public class IntervalProbabilityProvider : IIntervalProbabilityProvider
{
    IProbabilityProvider _probabilityProvider;
    private int MATCH_TIME = 90;

    public IntervalProbabilityProvider(IProbabilityProvider probabilityProvider)
    {
        _probabilityProvider = probabilityProvider;
    }
    public Dictionary<IntervalResult,double> GetIntervalProbability(int timeLeft, List<Team> currentGoalSequance, int intervalStart, int intervalEnd)
    {
        if (timeLeft<0 || timeLeft==90 && currentGoalSequance.Count!=0)
            throw new Exception("Bad data");
            
        Dictionary<IntervalResult,double> outcomeCounter = new Dictionary<IntervalResult, double>{
            {IntervalResult.GuestWin, 0},
            {IntervalResult.HomeWin, 0},
            {IntervalResult.NotClosed, 0},
            {IntervalResult.Tie, 0},
        };
        var currentScore = ToScore(currentGoalSequance);
        var time = MATCH_TIME - timeLeft;
        List<(int,int)> possibleScores = GetPosibbleScores(currentScore);
        foreach (var finalScore in possibleScores){
            var finalScoreProbability = _probabilityProvider.MatchScore(finalScore, currentScore, time);
            List<List<Team>> possibleEndingSequance = GetPossibleEnding(currentScore, finalScore);
            foreach (var endingSequance in possibleEndingSequance){
                IntervalResult intervalResult = GetIntervalResult(currentGoalSequance, endingSequance, intervalStart, intervalEnd);
                outcomeCounter[intervalResult]+=finalScoreProbability / possibleEndingSequance.Count;
            }
        }
        var total = outcomeCounter.Values.Sum();
        return outcomeCounter;
    }

    private IntervalResult GetIntervalResult(List<Team> currentGoalSequance, List<Team> ending, int intervalStart, int intervalEnd)
    {
        if (currentGoalSequance.Count + ending.Count< intervalEnd)
            return IntervalResult.NotClosed;

        var goalSequance = new List<Team>(currentGoalSequance);
        goalSequance.AddRange(ending);

        goalSequance = goalSequance.GetRange(intervalStart,intervalEnd-intervalStart);
        var homeTeamScore = goalSequance.Where(x=>x==Team.HomeTeam).Count();
        var guestTeamScore = goalSequance.Where(x=>x==Team.GuestTeam).Count();
        if (homeTeamScore>guestTeamScore)
            return IntervalResult.HomeWin;
        else if (homeTeamScore<guestTeamScore)
            return IntervalResult.GuestWin;
        else return IntervalResult.Tie;
    }

    private List<List<Team>> GetPossibleEnding((int, int) currentScore, (int, int) finalScore)
    {
        List<Team> ending = new List<Team>();
        ending.AddRange(Enumerable.Repeat(Team.HomeTeam, finalScore.Item1-currentScore.Item1));
        ending.AddRange(Enumerable.Repeat(Team.GuestTeam, finalScore.Item2-currentScore.Item2));
        return HelperFunctions.findPermutations(ending, 0, ending.Count);
    }

    private (int,int) ToScore(List<Team> goalSequance)
    {
        return (goalSequance.Where(x=>x==Team.HomeTeam).Count(), goalSequance.Where(x=>x==Team.GuestTeam).Count());
    }

    private List<(int, int)> GetPosibbleScores((int,int) currentScore)
    {
        return _probabilityProvider
            .GetPossibleScores()
            .Where(x=>x.Item1>=currentScore.Item1 && x.Item2>=currentScore.Item2)
            .ToList();
    }
    
}
