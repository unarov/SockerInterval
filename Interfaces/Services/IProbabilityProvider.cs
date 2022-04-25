using Interfaces.Models;
namespace Interfaces.Services;
public interface IProbabilityProvider
{
    public double MatchScore((int,int)matchScore);
    public double MatchScore((int,int) matchScore, (int,int) currentMatchScore, int time);
    public double TeamScore(Team team, int score);
}