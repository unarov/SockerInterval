using Interfaces;
using ProbabilityService;

// See https://aka.ms/new-console-template for more information

ProbabilityProvider matchStat = new ProbabilityProvider();


Console.WriteLine(matchStat.ProbabilityTeamScore(new TeamScore(Team.GuestTeam, 1)));

MatchScore ms = new MatchScore(Team.HomeTeam, Team.GuestTeam, 1, 2);
System.Console.WriteLine(matchStat.ProbabilityMatchScore(ms));