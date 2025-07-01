// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using TradingCatepillar.Alpaca.Connector.Services;
using TradingCatepillar.Alpaca.Connector.Services.Interfaces;
using TradingCatepillar.AlpacaAPI.Connector;
using TradingCatepillar.Core;
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

var worker = new InstrumentWorker(
    alpacaDataService: dataService,
    indicatorCalculationService: new IndicatorCalculationService(),
    gemini
);

await worker.WorkWithInstrument("MSFT");

var test = "";



