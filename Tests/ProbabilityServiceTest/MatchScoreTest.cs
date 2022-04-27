using NUnit.Framework;
using ProbabilityService;
using StatisticService;
using MatchMakerService;
using System.Collections.Generic;
using System.Linq;
using System;
using Interfaces.Models;

namespace ProbabilityServiceTest;
[TestFixture]
public class MatchScoreTest
{
    MatchMaker _matchMaker;
    int SEED = 32;
    int EXPERIMENTS_COUNT = 1000000;
    int MATCH_TIME = 90;
    double delta = 1e-3;
    private ProbabilityProvider _probabilityProvider;
    [SetUp]
    public void Setup()
    {
        var statistic = Statistic.GetMatchStat();
        _probabilityProvider = new ProbabilityProvider(statistic);
        _matchMaker = new MatchMaker(statistic,SEED);
    }

    [TestCase(3, 2)]
    [TestCase(0, 0)]
    [TestCase(5, 1)]
    [TestCase(5, 11)]
    public void Test(int teamOneScore, int teamTwoScore)
    {
        List<(int,int)> scoreList = new List<(int, int)>();
        for (int i=0; i<EXPERIMENTS_COUNT; i++)
            scoreList.Add(_matchMaker.GenerateScore());
            
        var expectedMatchScoreProbabilit = scoreList.Where(x=>x.Item1==teamOneScore && x.Item2==teamTwoScore).Count() * 1.0 / EXPERIMENTS_COUNT;
        var actualMatchScoreProbabilit = _probabilityProvider.MatchScore((teamOneScore,teamTwoScore));
        Assert.IsTrue(Math.Abs(expectedMatchScoreProbabilit-actualMatchScoreProbabilit)<delta);
        
    }
    [TestCase(3, 2, 1, 1, 30)]
    [TestCase(0, 0, 1, 4, 60)]
    [TestCase(5, 1, 3, 1, 75)]
    [TestCase(5, 11, 2, 4, 20)]
    [TestCase(2, 2, 2, 2, 90)]
    [TestCase(2, 2, 2, 3, 90)]
    public void Test(int teamOneScore, int teamTwoScore, int teamOneScoreAtTime, int teamTwoScoreAtTime, int time)
    {
        List<List<Goal>> scoreList = new List<List<Goal>>();
        for (int i=0; i<EXPERIMENTS_COUNT; i++)
            scoreList.Add(_matchMaker.GenerateGoalSequence());
            
        var targetScores = scoreList.Where(x=>ScoreAtTime(x,time)==(teamOneScoreAtTime,teamTwoScoreAtTime)).Select(x=>
            (x.Where(goal=>goal.team==Team.HomeTeam).Count(),
            x.Where(goal=>goal.team==Team.GuestTeam).Count())
        ).ToList();

        var expectedMatchScoreProbabilit = targetScores.Where(x=>x.Item1==teamOneScore && x.Item2==teamTwoScore).Count() * 1.0 / targetScores.Count;
        var actualMatchScoreProbabilit = _probabilityProvider.MatchScore((teamOneScore,teamTwoScore),(teamOneScoreAtTime,teamTwoScoreAtTime),time);
        System.Console.WriteLine(expectedMatchScoreProbabilit);
        System.Console.WriteLine(actualMatchScoreProbabilit);
        Assert.IsTrue(Math.Abs(expectedMatchScoreProbabilit-actualMatchScoreProbabilit)<delta);
        
    }
    private (int,int) ScoreAtTime(List<Goal> goalSequance, int time)
    {
        var teamOneScore = goalSequance.Count(x=>x.team==Team.HomeTeam&&x.time<=time);
        var teammTwoScore =  goalSequance.Count(x=>x.team==Team.GuestTeam&&x.time<=time);
        return (teamOneScore,teammTwoScore);
    }
}