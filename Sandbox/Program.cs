using Interfaces;
using ProbabilityService;
using MatchMakerService;

// See https://aka.ms/new-console-template for more information

MatchMaker matchMakerService = new MatchMaker();

for (int i=0; i<100; i++){
    var score = matchMakerService.GenerateScoreAtTime(1,(1,2));
    System.Console.WriteLine(score);
}
