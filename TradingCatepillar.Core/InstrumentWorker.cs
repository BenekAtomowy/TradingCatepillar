using System.Diagnostics.CodeAnalysis;
using TradingCatepillar.Alpaca.Connector.Services.Interfaces;
using TradingCatepillar.Core.Models;
using TradingCatepillar.Data.Analysis.Services.Interfaces;
using TradingCatepillar.Integration.GoogleGemini.Services.Interfaces;

namespace TradingCatepillar.Core
{
    public class InstrumentWorker
    {
        private readonly IAlpacaDataService _alpacaDataService;
        private readonly IIndicatorCalculationService _indicatorCalculationService;
        private readonly IGeminiHttpClient _geminiHttpClient;

        public InstrumentWorker(IAlpacaDataService alpacaDataService, IIndicatorCalculationService indicatorCalculationService, IGeminiHttpClient geminiHttpClient)
        {
            _alpacaDataService = alpacaDataService;
            _indicatorCalculationService = indicatorCalculationService;
            _geminiHttpClient = geminiHttpClient;
        }


        public async Task WorkWithInstrument(string symbol)
        {

            // Simulate fetching current snapshot
            var snapshot = await _alpacaDataService.GetCurrentSnapshotAsync(symbol);
            Console.WriteLine($"Current Price for {symbol}: {snapshot.LastPrice}");

            DateTime endTime = DateTime.UtcNow.AddDays(-1);
            DateTime startTime = endTime.AddDays(-7);
            // Simulate fetching historical data
            var historicalData = await _alpacaDataService.GetHistoricalDataAsync(symbol, TimeSpan.FromMinutes(5), startTime, endTime);
            Console.WriteLine($"Historical Data for {symbol}: {historicalData.Count} records");

            var data = historicalData.Select(c => new AnalysisMarketCandle() { Close = c.Close, Open = c.Open, High = c.High, Low = c.Low, TimeUtc = c.TimeUtc });

            var indicatorResults = _indicatorCalculationService.CalculateIndicators(data, 10);


            Console.WriteLine($"RSI: {indicatorResults.Rsi:F2}");
            Console.WriteLine($"EMA: {indicatorResults.Ema:F2}");
            Console.WriteLine($"SMA: {indicatorResults.Sma:F2}");
            Console.WriteLine($"ATR: {indicatorResults.Atr:F2}");
            Console.WriteLine($"MACD: {indicatorResults.Macd:F2}");

            var prompt = new InstrumentAIPromptModel(symbol, indicatorResults);

            var result = await _geminiHttpClient.AskForRecommendation(prompt);

        }

    }
}
