using Interfaces;
using ProbabilityService;

// See https://aka.ms/new-console-template for more information

ProbabilityProvider probabilityProvider = new ProbabilityProvider();


Console.WriteLine(probabilityProvider.ProbabilityTeamScore(new TeamScore(Team.GuestTeam, 1)));

MatchScore ms = new MatchScore(Team.HomeTeam, Team.GuestTeam, 1, 2);
MatchScore ms_2 = new MatchScore(Team.GuestTeam,Team.HomeTeam,  1, 2);
MatchScore ms_3 = new MatchScore(Team.GuestTeam,Team.HomeTeam,  2, 1);

System.Console.WriteLine(probabilityProvider.ProbabilityMatchScore(ms));
System.Console.WriteLine(probabilityProvider.ProbabilityMatchScore(ms_2));
System.Console.WriteLine(probabilityProvider.ProbabilityMatchScore(ms_3));

System.Console.WriteLine(ms==ms_3);