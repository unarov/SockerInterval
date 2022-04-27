using NUnit.Framework;
using  IntervalService;
using MatchMakerService;
using System.Collections.Generic;
using Interfaces.Models;
using System;
using System.Linq;

namespace IntervalServiceTest;

public class IntervalProbabilityProviderTest
{
    IntervalProbabilityProvider _intervalProbabilityProvider;
    MatchMaker _matchMaker;
    int SEED = 33;
    int EXPERIMENTS_COUNT = 1000000;
    double delta = 1e-2;
    private static readonly object[] _intervalProbabilityTestCases = new []{
            new object[]{0, new List<Team>(){Team.HomeTeam,Team.GuestTeam}, 0, 0},
            new object[]{90, new List<Team>(), 1, 3},
            new object[]{0, new List<Team>(){Team.HomeTeam,Team.GuestTeam}, 0, 1},
            new object[]{10, new List<Team>(){Team.HomeTeam,Team.GuestTeam}, 2, 5},
            new object[]{90, new List<Team>(), 3, 4},
            new object[]{45, new List<Team>(){Team.HomeTeam,Team.GuestTeam}, 2, 3},
            new object[]{80, new List<Team>(){Team.GuestTeam}, 2, 3},
            new object[]{10, new List<Team>(){Team.HomeTeam,Team.GuestTeam,Team.GuestTeam}, 2, 5},
            new object[]{40, new List<Team>(){Team.HomeTeam,Team.GuestTeam}, 2, 4}
        };
    [SetUp]
    public void Setup()
    {
        var statistic = StatisticService.Statistic.GetMatchStat();
        _matchMaker = new MatchMaker(statistic, SEED);
        var probabilityProvider = new ProbabilityService.ProbabilityProvider(statistic);
        _intervalProbabilityProvider = new IntervalProbabilityProvider(probabilityProvider);
    }
    [TestCaseSource("_intervalProbabilityTestCases")]
    public void IntervalProbabilityTest(int timeLeft, List<Team> goalSequance, int intervalStart, int intervalEnd)
    {
        var time = 90 - timeLeft;
        List<IntervalResult> intervalResults = new List<IntervalResult>();

        for (int i=0; i<EXPERIMENTS_COUNT; i++){
            var  generatedGoalSequance = _matchMaker.GenerateGoalSequence();
            if (ValidGoalSequance(generatedGoalSequance, goalSequance, time))
                intervalResults.Add(GetIntervalResult(generatedGoalSequance.Select(x=>x.team).ToList(), intervalStart, intervalEnd));
        }

        double expectedHomeWin = intervalResults.Where(x=>x==IntervalResult.HomeWin).Count() * 1.0 / intervalResults.Count;
        double expectedGuestWin = intervalResults.Where(x=>x==IntervalResult.GuestWin).Count() * 1.0 / intervalResults.Count;
        double expectedTie = intervalResults.Where(x=>x==IntervalResult.Tie).Count() * 1.0 / intervalResults.Count;
        double expectedNotClosed = intervalResults.Where(x=>x==IntervalResult.NotClosed).Count() * 1.0 / intervalResults.Count;
        System.Console.WriteLine("expected");
        System.Console.WriteLine(expectedHomeWin);
        System.Console.WriteLine(expectedGuestWin);
        System.Console.WriteLine(expectedTie);
        System.Console.WriteLine(expectedNotClosed);
        
        var actualInterval = _intervalProbabilityProvider.GetIntervalProbability(timeLeft, goalSequance, intervalStart, intervalEnd);

        System.Console.WriteLine("actual");
        System.Console.WriteLine(actualInterval[IntervalResult.HomeWin]);
        System.Console.WriteLine(actualInterval[IntervalResult.GuestWin]);
        System.Console.WriteLine(actualInterval[IntervalResult.Tie]);
        System.Console.WriteLine(actualInterval[IntervalResult.NotClosed]);

        var probSum = actualInterval[IntervalResult.HomeWin]
            + actualInterval[IntervalResult.GuestWin]
            + actualInterval[IntervalResult.Tie]
            + actualInterval[IntervalResult.NotClosed];

        Assert.IsTrue(Math.Abs(expectedHomeWin-actualInterval[IntervalResult.HomeWin])<delta, "Expected HomeWin not reached");
        Assert.IsTrue(Math.Abs(expectedGuestWin-actualInterval[IntervalResult.GuestWin])<delta, "Expected GuestWin not reached");
        Assert.IsTrue(Math.Abs(expectedTie-actualInterval[IntervalResult.Tie])<delta, "Expected Tie not reached");
        Assert.IsTrue(Math.Abs(expectedNotClosed-actualInterval[IntervalResult.NotClosed])<delta, "Expected NotClosed not reached");
        Assert.IsTrue(Math.Abs(probSum - 1) < delta, "Probability sum should be 1");
    }

    private bool ValidGoalSequance(List<Goal> generatedGoalSequance, List<Team> goalSequance, int time)
    {
        return generatedGoalSequance
            .OrderBy(x=>x.time)
            .Where(x=>x.time<=time)
            .Select(x=>x.team)
            .SequenceEqual(goalSequance);
        
    }

    private IntervalResult GetIntervalResult(List<Team> goalSequance, int intervalStart, int intervalEnd)
    {
        if(goalSequance.Count<intervalEnd)
            return IntervalResult.NotClosed;

        var intervalSequance = goalSequance.GetRange(intervalStart,intervalEnd-intervalStart);

        var homeTeamGoals = intervalSequance.Where(x=>x==Team.HomeTeam).Count();
        var guestTeamGoals = intervalSequance.Where(x=>x==Team.GuestTeam).Count();
        if (homeTeamGoals>guestTeamGoals)
            return IntervalResult.HomeWin;
        else if (guestTeamGoals>homeTeamGoals)
            return IntervalResult.GuestWin;
        else 
            return IntervalResult.Tie;
    }
}