using Interfaces;

namespace ProbabilityService;
public class MatchStat
{
    Dictionary<(int,int),int> statistic;
    public MatchStat()
    {
        statistic = new Dictionary<(int,int), int>();
    }
    public void Add((int,int) score, int count)
    {
        if (statistic.ContainsKey(score)){
            statistic[score]+=count;
        }
        else{
            statistic[score]=count;
        }
    }
    public double ScorePercentage((int,int) matchScore)
    {
        int totalMatches = statistic.Sum(x=>x.Value);
        return statistic[matchScore] * 1.0 / totalMatches;
    }

    public double TeamScorePercentage(Team team, int score)
    {
        if (team != Team.HomeTeam &&  team != Team.GuestTeam)
            throw new Exception("Unknown Team");

        var totalMatches = statistic.Sum(x=>x.Value);
        int targetMatches;
        if (team==Team.HomeTeam)
            targetMatches = statistic.Where(x=>x.Key.Item1==score).Sum(x=>x.Value);
        else
            targetMatches = statistic.Where(x=>x.Key.Item2==score).Sum(x=>x.Value);
        return targetMatches * 1.0 / totalMatches;
    }

    public IEnumerable<(int,int)> GetPosibleOutcomes()
    {
        return statistic.Keys;
    }
    public static MatchStat GetMatchStat()
    {
        MatchStat matchStat = new MatchStat();

        matchStat.Add((0,0), 433);
        matchStat.Add((0,1), 605);
        matchStat.Add((0,2), 424);
        matchStat.Add((0,3), 198);
        matchStat.Add((0,4), 70);
        matchStat.Add((0,5), 19);

        matchStat.Add((1,0), 757);
        matchStat.Add((1,1), 1059);
        matchStat.Add((1,2), 741);
        matchStat.Add((1,3), 346);
        matchStat.Add((1,4), 121);
        matchStat.Add((1,5), 34);

        matchStat.Add((2,0), 662);
        matchStat.Add((2,1), 927);
        matchStat.Add((2,2), 648);
        matchStat.Add((2,3), 303);
        matchStat.Add((2,4), 106);
        matchStat.Add((2,5), 29);

        matchStat.Add((3,0), 386);
        matchStat.Add((3,1), 541);
        matchStat.Add((3,2), 378);
        matchStat.Add((3,3), 176);
        matchStat.Add((3,4), 62);
        matchStat.Add((3,5), 17);

        matchStat.Add((4,0), 168);
        matchStat.Add((4,1), 236);
        matchStat.Add((4,2), 165);
        matchStat.Add((4,3), 78);
        matchStat.Add((4,4), 27);

        matchStat.Add((5,0), 59);
        matchStat.Add((5,1), 83);
        matchStat.Add((5,2), 57);
        matchStat.Add((5,3), 27);

        matchStat.Add((6,0), 17);
        matchStat.Add((6,1), 24);
        matchStat.Add((6,2), 17);

        return matchStat;
    }
}