// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradingCatepillar.AlpacaAPI.Connector;
using TradingCatepillar.Core.Builders;

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

var workerBuilder = host.Services.GetRequiredService<InstrumentWorkerBuilder>();

var worker = workerBuilder.BuildWorker("MSFT");
var worker2 = workerBuilder.BuildWorker("AAPL");
var worker3 = workerBuilder.BuildWorker("LMT");

await worker.WorkWithInstrument();
await worker2.WorkWithInstrument();
await worker3.WorkWithInstrument();

var test = "";



