// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradingCatepillar.AlpacaAPI.Connector;
using TradingCatepillar.Core.Builders;
using TradingCatepillar.Core.Services.Interafaces;

Console.WriteLine("Trading Catepillar - console app");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddTradingCatepillarServices();
    })
    .Build();


await host.Services.GetRequiredService<AlpacaAPIClient>().Initialize();

var workerService = host.Services.GetRequiredService<IInstrumentWorkerService>();

workerService.AddWorker("MSFT");
//workerService.AddWorker("AAPL");
//workerService.AddWorker("LMT");
//workerService.AddWorker("GOOGL");
//workerService.AddWorker("AMZN");
//workerService.AddWorker("TSLA");
//workerService.AddWorker("NFLX");
//workerService.AddWorker("META");
//workerService.AddWorker("NVDA");
//workerService.AddWorker("AMD");
//workerService.AddWorker("INTC");
//workerService.AddWorker("CSCO");


await workerService.WorkAllAsync();

var test = "";



