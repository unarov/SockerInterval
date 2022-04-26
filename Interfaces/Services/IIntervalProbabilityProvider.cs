using Interfaces.Models;
namespace Interfaces.Services;
public interface IIntervalProbabilityProvider
{
    public Dictionary<IntervalResult,double> GetIntervalProbability(int timeLeft, List<Goal> goalSequance, int intervalStart, int intervalEnd);
}