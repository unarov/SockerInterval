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
    int SEED = 32;
    int EXPERIMENTS_COUNT = 1000000;
    double delta = 1e-3;
    private static readonly object[] _intervalProbabilityTestCases = new []{
            new object[]{90, new List<Goal>(), 1, 3},
            new object[]{45, new List<Goal>(), 2, 3},
            new object[]{10, new List<Goal>(), 2, 5}
        };
    [SetUp]
    public void Setup()
    {
        var statistic = StatisticService.Statistic.GetMatchStat();
        _matchMaker = new MatchMaker(statistic, SEED);
        _intervalProbabilityProvider = new IntervalProbabilityProvider();
    }
    [TestCaseSource("_intervalProbabilityTestCases")]
    public void IntervalProbabilityTest(int timeLeft, List<Goal> goalSequance, int intervalStart, int intervalEnd)
    {
        List<IntervalResult> intervalResults = new List<IntervalResult>();

        for (int i=0; i<EXPERIMENTS_COUNT; i++){
            var matchGoalSequance = _matchMaker.GenerateGoalSequence();
            intervalResults.Add(GetIntervalResult(matchGoalSequance, intervalStart, intervalEnd));
        }

        double expectedHomeWin = intervalResults.Where(x=>x==IntervalResult.HomeWin).Count() * 1.0 / EXPERIMENTS_COUNT;
        double expectedGuestWin = intervalResults.Where(x=>x==IntervalResult.GuestWin).Count() * 1.0 / EXPERIMENTS_COUNT;
        double expectedTie = intervalResults.Where(x=>x==IntervalResult.Tie).Count() * 1.0 / EXPERIMENTS_COUNT;
        double expectedNotClosed = intervalResults.Where(x=>x==IntervalResult.NotClosed).Count() * 1.0 / EXPERIMENTS_COUNT;

        System.Console.WriteLine(expectedHomeWin);
        System.Console.WriteLine(expectedGuestWin);
        System.Console.WriteLine(expectedTie);
        System.Console.WriteLine(expectedNotClosed);

        var actualInterval = _intervalProbabilityProvider.GetIntervalProbability(timeLeft, goalSequance, intervalStart, intervalEnd);

        Assert.IsTrue(Math.Abs(expectedHomeWin-actualInterval[IntervalResult.HomeWin])<delta, "Expected HomeWin not reached");
        Assert.IsTrue(Math.Abs(expectedGuestWin-actualInterval[IntervalResult.GuestWin])<delta, "Expected GuestWin not reached");
        Assert.IsTrue(Math.Abs(expectedTie-actualInterval[IntervalResult.Tie])<delta, "Expected Tie not reached");
        Assert.IsTrue(Math.Abs(expectedNotClosed-actualInterval[IntervalResult.NotClosed])<delta, "Expected NotClosed not reached");
    }
    private IntervalResult GetIntervalResult(List<Goal> goalSequance, int intervalStart, int intervalEnd)
    {
        if(goalSequance.Count<=intervalEnd)
            return IntervalResult.NotClosed;

        var intervalSequance = goalSequance.GetRange(intervalStart,intervalEnd-intervalStart);

        var homeTeamGoals = intervalSequance.Where(x=>x.team==Team.HomeTeam).Count();
        var guestTeamGoals = intervalSequance.Where(x=>x.team==Team.GuestTeam).Count();
        if (homeTeamGoals>guestTeamGoals)
            return IntervalResult.HomeWin;
        if (guestTeamGoals>homeTeamGoals)
            return IntervalResult.GuestWin;
        return IntervalResult.Tie;
    }
}