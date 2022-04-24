using Interfaces.Services;
using MatchMakerService;
using ProbabilityService;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = new ServiceCollection()
    .AddSingleton<IStatistic>(StatisticService.Statistic.GetMatchStat())
    .AddScoped<IMatchMaker, MatchMaker>()
    .AddScoped<IProbabilityProvider,ProbabilityProvider>()
    .BuildServiceProvider();

var tastService = serviceProvider.GetRequiredService<IMatchMaker>();
System.Console.WriteLine(tastService.GenerateScore());

