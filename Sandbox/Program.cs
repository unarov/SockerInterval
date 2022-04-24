using Interfaces.Services;
using MatchMakerService;
using ProbabilityService;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = new ServiceCollection()
    .AddSingleton<IStatistic>(StatisticService.Statistic.GetMatchStat())
    .AddScoped<IMatchMaker, MatchMaker>()
    .AddScoped<IProbabilityProvider,ProbabilityProvider>()
    .BuildServiceProvider();

var probabilityProvider = serviceProvider.GetRequiredService<IProbabilityProvider>();
var foo =probabilityProvider.MatchScoreIfCurrentMatchScore((2,2),(1,1),40);
System.Console.WriteLine(foo);

