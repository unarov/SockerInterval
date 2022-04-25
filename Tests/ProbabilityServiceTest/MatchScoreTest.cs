using NUnit.Framework;
using ProbabilityService;
using StatisticService;
using MatchMakerService;
using System.Collections.Generic;
using System.Linq;
using System;

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
}