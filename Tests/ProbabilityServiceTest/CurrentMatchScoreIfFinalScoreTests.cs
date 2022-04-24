using NUnit.Framework;
using ProbabilityService;

namespace ProbabilityServiceTest;
[TestFixture]
public class CurrentMatchScoreIfFinalScoreTests
{
    private ProbabilityProvider _probabilityProvider;
    [SetUp]
    public void Setup()
    {
        _probabilityProvider = new ProbabilityProvider();
    }

    [Test]
    public void CurrentMatchScoreIfFinalScoreTest()
    {
        
        Assert.Pass();
    }
}