using Interfaces.Services;
using MatchMakerService;
using ProbabilityService;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = new ServiceCollection()
    .AddSingleton<IStatistic>(StatisticService.Statistic.GetMatchStat())
    .AddScoped<IMatchMaker, MatchMaker>()
    .AddScoped<IProbabilityProvider,ProbabilityProvider>()
    .BuildServiceProvider();

var matchMakerService = serviceProvider.GetService<IMatchMaker>();
for (int i=0; i<100; i++){
    var score = matchMakerService.GenerateScoreAtTime(1,(1,2));
    System.Console.WriteLine(score);
}
