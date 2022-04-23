﻿using Interfaces;
using MatchMakerService;

// See https://aka.ms/new-console-template for more information

MatchStat matchStat = new MatchStat();
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 0,0), 433);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 0,1), 605);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 0,2), 424);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 0,3), 198);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 0,4), 70);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 0,5), 19);

matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 1,0), 757);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 1,1), 1059);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 1,2), 741);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 1,3), 346);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 1,4), 121);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 1,5), 34);

matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 2,0), 662);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 2,1), 927);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 2,2), 648);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 2,3), 303);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 2,4), 106);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 2,5), 29);

matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 3,0), 386);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 3,1), 541);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 3,2), 378);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 3,3), 176);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 3,4), 62);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 3,5), 17);

matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 4,0), 168);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 4,1), 236);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 4,2), 165);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 4,3), 78);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 4,4), 27);

matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 5,0), 59);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 5,1), 83);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 5,2), 57);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 5,3), 27);

matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 6,0), 17);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 6,1), 24);
matchStat.Add(new MatchScore(Team.HomeTeam,Team.GuestTeam, 6,2), 17);

Console.WriteLine(matchStat.GetProbability(Team.HomeTeam, 3));