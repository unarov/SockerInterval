using NUnit.Framework;
using ProbabilityService;
using StatisticService;
using Interfaces.Models;
using MatchMakerService;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ProbabilityServiceTest;
[TestFixture]
public class TeamScoreTest
{
    MatchMaker _matchMaker;
    int SEED = 32;
    int EXPERIMENTS_COUNT = 1000000;
    double delta = 1e-3;
    private ProbabilityProvider _probabilityProvider;
    [SetUp]
    public void Setup()
    {
        var statistic = Statistic.GetMatchStat();
        _probabilityProvider = new ProbabilityProvider(statistic);
        _matchMaker = new MatchMaker(statistic,SEED);
    }

    [TestCase(Team.HomeTeam, 2)]
    [TestCase(Team.HomeTeam, 4)]
    [TestCase(Team.GuestTeam, 0)]
    public void Test(Team team, int score)
    {
        List<(int,int)> scoreList = new List<(int, int)>();
        for (int i=0; i<EXPERIMENTS_COUNT; i++)
            scoreList.Add(_matchMaker.GenerateScore());
        double expected;
        if (team==Team.HomeTeam)
            expected = scoreList.Where(x=>x.Item1==score).Count() * 1.0 / EXPERIMENTS_COUNT;
        else
            expected = scoreList.Where(x=>x.Item2==score).Count() * 1.0 / EXPERIMENTS_COUNT;
        var  actualProbability = _probabilityProvider.TeamScore(team, score);
        Assert.IsTrue(Math.Abs(actualProbability-expected)<delta);
    }
}