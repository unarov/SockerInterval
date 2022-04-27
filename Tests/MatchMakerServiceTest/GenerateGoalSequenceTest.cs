using NUnit.Framework;
using MatchMakerService;
using System.Linq;
using System;
using Interfaces.Models;
using System.Collections.Generic;

namespace MatchMakerServiceTest;
[TestFixture]
public class GenerateGoalSequenceTest
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
    [Test]
    public void firstGoalTimeIsZero()
    {
        var scores = new List<List<Goal>>();
        for (int i=0; i<EXPERIMENTS_COUNT; i++)
            scores.Add(_matchMaker.GenerateGoalSequence());
        var firstGoalTime = scores.Where(x=>x.Count>0).Select(x=>x.Min(x=>x.time)).Min();
        Assert.IsTrue(firstGoalTime==0);
    }
    [Test]
    public void maxGoalTimeIs89()
    {
        var scores = new List<List<Goal>>();
        for (int i=0; i<EXPERIMENTS_COUNT; i++)
            scores.Add(_matchMaker.GenerateGoalSequence());
        var maxGoalTime = scores.Where(x=>x.Count>0).Select(x=>x.Max(x=>x.time)).Max();
        Assert.IsTrue(maxGoalTime==89);
    }
}