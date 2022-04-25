using NUnit.Framework;
using ProbabilityService;
using StatisticService;
using Interfaces.Models;

namespace ProbabilityServiceTest;
[TestFixture]
public class TeamScoreTest
{
    private ProbabilityProvider _probabilityProvider;
    [SetUp]
    public void Setup()
    {
        var statistic = Statistic.GetMatchStat();
        _probabilityProvider = new ProbabilityProvider(statistic);
    }

    [TestCase(Team.HomeTeam, 2, 0.2675)]
    [TestCase(Team.HomeTeam, 4, 0.0674)]
    [TestCase(Team.GuestTeam, 0, 0.2482)]
    public void Test(Team team, int score, double expected)
    {
        var  actualProbability = _probabilityProvider.TeamScore(team, score);
        Assert.IsTrue(actualProbability==expected);
    }
}