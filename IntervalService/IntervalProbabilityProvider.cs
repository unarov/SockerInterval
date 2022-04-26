using Interfaces.Models;
using Interfaces.Services;

namespace IntervalService;
public class IntervalProbabilityProvider : IIntervalProbabilityProvider
{
    public Dictionary<IntervalResult,double> GetIntervalProbability(int timeLeft, List<Goal> goalSequance, int intervalStart, int intervalEnd)
    {
        throw new NotImplementedException();
    }
}
