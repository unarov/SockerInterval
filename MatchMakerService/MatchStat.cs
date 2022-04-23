using Interfaces;

namespace MatchMakerService;
public class MatchStat
{
    Dictionary<MatchScore,int> statistic;
    public MatchStat()
    {
        statistic = new Dictionary<MatchScore, int>();
    }
    public void Add(MatchScore score, int count)
    {
        if (statistic.ContainsKey(score)){
            statistic[score]+=count;
        }
        else{
            statistic[score]=count;
        }
    }
    public double GetProbability(Team team, int goals)
    {
        int totalMatches = statistic.Sum(x=>x.Value);
        int targetMatches = statistic.Where(x=>x.Key.GetTeamScore(team)==goals).Sum(x=>x.Value);
        return targetMatches*1.0 / totalMatches;
    }
}