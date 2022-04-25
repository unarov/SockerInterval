using Interfaces.Services;
using Interfaces.Models;
using MatchMakerService;
using ProbabilityService;
using Microsoft.Extensions.DependencyInjection;




var serviceProvider = new ServiceCollection()
    .AddSingleton<IStatistic>(StatisticService.Statistic.GetMatchStat())
    .AddSingleton<IMatchMaker,MatchMaker>()
    .AddScoped<IProbabilityProvider,ProbabilityProvider>()
    .BuildServiceProvider();

var testService = serviceProvider.GetRequiredService<IMatchMaker>();

for (int i=0; i<10; i++){
var foo = testService.GenerateGoalSequence();
    System.Console.WriteLine("Start of match");
    foreach (var goal in foo)
        System.Console.WriteLine(goal.team + " at " + goal.time);
    System.Console.WriteLine("End of match");
    System.Console.WriteLine("--------------------------------");

}