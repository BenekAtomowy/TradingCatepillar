// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using TradingCatepillar.Alpaca.Connector.Services;
using TradingCatepillar.Alpaca.Connector.Services.Interfaces;
using TradingCatepillar.AlpacaAPI.Connector;
using TradingCatepillar.Core;
using TradingCatepillar.Core.Builders;
using TradingCatepillar.Core.Services;
using TradingCatepillar.Core.Services.Interafaces;
using TradingCatepillar.Data.Analysis.Services;
using TradingCatepillar.Integration.GoogleGemini.Services;

Console.WriteLine("Trading Catepillar - console app");

var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // Set the base path
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

// Access configuration values
var client = new AlpacaAPIClient(configuration);
await client.Initialize();
IAlpacaDataService dataService = new AlpacaDataService(client.DataClient);
IAlpacaTradeService tradeService = new AlpacaTradeService(client.TradingClient);

//create new http client
var httpClient = new HttpClient();

var gemini = new GeminiHttpClient(httpClient, configuration);
var aiRecommendationService = new AIRecommendationService(gemini, new AIPromptBuilder());

var workerBuilder = new InstrumentWorkerBuilder(
    alpacaDataService: dataService,
    indicatorCalculationService: new IndicatorCalculationService(),
    aiRecommendationService
);

var worker = workerBuilder.BuildWorker("MSFT");
var worker2 = workerBuilder.BuildWorker("AAPL");
var worker3 = workerBuilder.BuildWorker("LMT");

await worker.WorkWithInstrument();
await worker2.WorkWithInstrument();
await worker3.WorkWithInstrument();

var test = "";



