using Interfaces;
using ProbabilityService;

// See https://aka.ms/new-console-template for more information

ProbabilityProvider probabilityProvider = new ProbabilityProvider();


System.Console.WriteLine(probabilityProvider.CurrentMatchScoreIfFinalScore((2,2),40,(3,3)));
System.Console.WriteLine(probabilityProvider.CurrentMatchScoreIfFinalScore((2,2),40,(3,2)));
System.Console.WriteLine(probabilityProvider.CurrentMatchScoreIfFinalScore((2,2),40,(2,2)));