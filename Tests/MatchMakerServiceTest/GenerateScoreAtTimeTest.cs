using NUnit.Framework;
using MatchMakerService;
using System.Linq;
using System;

namespace MatchMakerServiceTest;
[TestFixture]
public class GenerateScoreAtTimeTest
{
    MatchMaker _matchMaker;
    int SEED = 32;
    int EXPERIMENTS_COUNT = 1000000;
    int MATCH_TIME = 90;
    double delta = 1e-3;
    [SetUp]
    public void Setup()
    {
        var statistic = StatisticService.Statistic.GetMatchStat();
        _matchMaker = new MatchMaker(statistic, SEED);
    }

    [TestCase(3, 2, 70)]
    [TestCase(1, 2, 20)]
    [TestCase(2, 0, 88)]
    public void Test(int teamOneScore,int teamTwoScore, int time)
    {
        var finalScore = (teamOneScore,teamTwoScore);
        var scores = new System.Collections.Generic.List<(int,int)>();
        for (int i=0; i<EXPERIMENTS_COUNT; i++)
            scores.Add(_matchMaker.GenerateScoreAtTime(time,finalScore));
        var actualMean1 = scores.Average(x=>x.Item1);
        var expectedMean1 = finalScore.Item1 * time * 1.0 / MATCH_TIME;
        Assert.IsTrue(Math.Abs(actualMean1-expectedMean1)<delta);
        
        var actualMean2 = scores.Average(x=>x.Item2);
        var expectedMean2 = finalScore.Item2 * time * 1.0 / MATCH_TIME;
        Assert.IsTrue(Math.Abs(actualMean2-expectedMean2)<delta);
    }

    [TestCase(88)]
    [TestCase(30)]
    [TestCase(12)]
    public void Test2(int time)
    {
        var finalScore = _matchMaker.GenerateScore();
        Test(finalScore.Item1, finalScore.Item2, time);
    }
}