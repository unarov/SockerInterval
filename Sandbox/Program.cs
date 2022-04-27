using Interfaces.Services;
using Interfaces.Models;
using MatchMakerService;
using ProbabilityService;
using Microsoft.Extensions.DependencyInjection;
using IntervalService;




var serviceProvider = new ServiceCollection()
    .AddSingleton<IStatistic>(StatisticService.Statistic.GetMatchStat())
    .AddSingleton<IMatchMaker,MatchMaker>()
    .AddScoped<IProbabilityProvider,ProbabilityProvider>()
    .AddScoped<IIntervalProbabilityProvider,IntervalProbabilityProvider>()
    .BuildServiceProvider();

var testService = serviceProvider.GetRequiredService<IIntervalProbabilityProvider>();

var bar = testService.GetIntervalProbability(10, new List<Team>(), 2, 5);
foreach (var foo in bar){
    System.Console.Write(foo.Key+ " <-> ");
    System.Console.WriteLine(foo.Value);
}