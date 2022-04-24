using Interfaces;
using ProbabilityService;

// See https://aka.ms/new-console-template for more information

ProbabilityProvider probabilityProvider = new ProbabilityProvider();


System.Console.WriteLine(probabilityProvider.CurrentMatchScoreIfFinalScore((1,2),90,(1,2)));