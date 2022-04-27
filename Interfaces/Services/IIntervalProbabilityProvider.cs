using Interfaces.Models;
namespace Interfaces.Services;
public interface IIntervalProbabilityProvider
{
    public Dictionary<IntervalResult,double> GetIntervalProbability(int timeLeft, List<Team> goalSequance, int intervalStart, int intervalEnd);
}