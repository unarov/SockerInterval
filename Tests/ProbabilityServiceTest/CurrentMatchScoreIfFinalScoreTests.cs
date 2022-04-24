using NUnit.Framework;
using ProbabilityService;
using StatisticService;

namespace ProbabilityServiceTest;
[TestFixture]
public class CurrentMatchScoreIfFinalScoreTests
{
    private ProbabilityProvider _probabilityProvider;
    [SetUp]
    public void Setup()
    {
        var statistic = Statistic.GetMatchStat();
        _probabilityProvider = new ProbabilityProvider(statistic);
    }

    [Test]
    public void CurrentMatchScoreIfFinalScoreTest()
    {
        
        Assert.Pass();
    }
}