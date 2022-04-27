using Interfaces.Models;
using Interfaces.Services;

namespace StatisticService;
public class Statistic : IStatistic
{
    private Dictionary<(int,int),int> _statistic;
    public Statistic()
    {
        _statistic = new Dictionary<(int,int), int>();
    }
    public void Add((int,int) score, int count)
    {
        if (_statistic.ContainsKey(score)){
            _statistic[score]+=count;
        }
        else{
            _statistic[score]=count;
        }
    }
    public double ScorePercentage((int,int) matchScore)
    {
        if (!_statistic.Keys.Contains(matchScore))
            return 0;
        int totalMatches = _statistic.Sum(x=>x.Value);
        return _statistic[matchScore] * 1.0 / totalMatches;
    }

    public double TeamScorePercentage(Team team, int score)
    {
        if (team != Team.HomeTeam &&  team != Team.GuestTeam)
            throw new Exception("Unknown Team");

        var totalMatches = _statistic.Sum(x=>x.Value);
        int targetMatches;
        if (team==Team.HomeTeam)
            targetMatches = _statistic.Where(x=>x.Key.Item1==score).Sum(x=>x.Value);
        else
            targetMatches = _statistic.Where(x=>x.Key.Item2==score).Sum(x=>x.Value);
        return targetMatches * 1.0 / totalMatches;
    }

    public IEnumerable<(int,int)> GetOutcomes()
    {
        return _statistic.Keys;
    }    
    public Dictionary<(int, int), int> GetMatchesCount()
    {
        return _statistic;
    }
    public static Statistic GetMatchStat()
    {
        Statistic statistic = new Statistic();

        statistic.Add((0,0), 433);
        statistic.Add((0,1), 605);
        statistic.Add((0,2), 424);
        statistic.Add((0,3), 198);
        statistic.Add((0,4), 70);
        statistic.Add((0,5), 19);

        statistic.Add((1,0), 757);
        statistic.Add((1,1), 1059);
        statistic.Add((1,2), 741);
        statistic.Add((1,3), 346);
        statistic.Add((1,4), 121);
        statistic.Add((1,5), 34);

        statistic.Add((2,0), 662);
        statistic.Add((2,1), 927);
        statistic.Add((2,2), 648);
        statistic.Add((2,3), 303);
        statistic.Add((2,4), 106);
        statistic.Add((2,5), 29);

        statistic.Add((3,0), 386);
        statistic.Add((3,1), 541);
        statistic.Add((3,2), 378);
        statistic.Add((3,3), 176);
        statistic.Add((3,4), 62);
        statistic.Add((3,5), 17);

        statistic.Add((4,0), 168);
        statistic.Add((4,1), 236);
        statistic.Add((4,2), 165);
        statistic.Add((4,3), 78);
        statistic.Add((4,4), 27);

        statistic.Add((5,0), 59);
        statistic.Add((5,1), 83);
        statistic.Add((5,2), 57);
        statistic.Add((5,3), 27);

        statistic.Add((6,0), 17);
        statistic.Add((6,1), 24);
        statistic.Add((6,2), 17);

        return statistic;
    }

}