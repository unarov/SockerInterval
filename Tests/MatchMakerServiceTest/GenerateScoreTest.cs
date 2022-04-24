using NUnit.Framework;
using MatchMakerService;
using System.Linq;
using System;
using System.Collections.Generic;

namespace MatchMakerServiceTest;
[TestFixture]
public class GenerateScoreTest
{
    MatchMaker _matchMaker;
    int SEED = 32;
    int EXPERIMENTS_COUNT = 1000000;
    double delta = 1e-3;
    [SetUp]
    public void Setup()
    {
        var statistic = StatisticService.Statistic.GetMatchStat();
        _matchMaker = new MatchMaker(statistic, SEED);
    }

    [TestCase(3, 2, 0.0378)]
    [TestCase(1, 2, 0.0741)]
    [TestCase(2, 0, 0.0662)]
    [TestCase(8, 9, 0)]
    public void Test(int teamOneScore,int teamTwoScore, double expected)
    {
        var targetScore = (teamOneScore,teamTwoScore);
        Dictionary<(int,int),int> generatedScores = new Dictionary<(int, int), int>();
        for (int i=0; i<EXPERIMENTS_COUNT; i++){
            var  score = _matchMaker.GenerateScore();
            if (generatedScores.Keys.Contains(score))
                generatedScores[score]++;
            else
                generatedScores[score] = 1;
        }
        double actualScorePercentage;
        if (!generatedScores.Keys.Contains(targetScore))
            actualScorePercentage = 0;
        else
            actualScorePercentage = generatedScores[targetScore]*1.0/EXPERIMENTS_COUNT;
        Assert.IsTrue(Math.Abs(actualScorePercentage-expected)<delta);
    }
}