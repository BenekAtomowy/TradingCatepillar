using TradingCatepillar.Alpaca.Connector.Services.Interfaces;
using TradingCatepillar.Core.Models;
using TradingCatepillar.Core.Services.Interafaces;
using TradingCatepillar.Data.Analysis.Services.Interfaces;

namespace TradingCatepillar.Core.Workers
{
    public class InstrumentWorker
    {
        private readonly IAlpacaDataService _alpacaDataService;
        private readonly IIndicatorCalculationService _indicatorCalculationService;
        private readonly IAIRecommendationService _aiRecommendationService;

        private string? _symbol;


        public InstrumentWorker(IAlpacaDataService alpacaDataService, IIndicatorCalculationService indicatorCalculationService, IAIRecommendationService aiRecommendationService)
        {
            _alpacaDataService = alpacaDataService;
            _indicatorCalculationService = indicatorCalculationService;
            _aiRecommendationService = aiRecommendationService;
        }

        internal void Initialize(string symbol)
        {
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol), "Symbol cannot be null or empty.");
        }

        public async Task WorkWithInstrument()
        {
            if (_symbol is null)
            {
                throw new InvalidOperationException("Symbol is not initialized. Call Initialize method with a valid symbol before working with the instrument.");
            }

            // Simulate fetching current snapshot
            var snapshot = await _alpacaDataService.GetCurrentSnapshotAsync(_symbol);
            Console.WriteLine($"Current Price for {_symbol}: {snapshot.LastPrice}");

            DateTime endTime = DateTime.UtcNow.AddDays(-1);
            DateTime startTime = endTime.AddDays(-7);
            // Simulate fetching historical data
            var historicalData = await _alpacaDataService.GetHistoricalDataAsync(_symbol, TimeSpan.FromMinutes(5), startTime, endTime);
            Console.WriteLine($"Historical Data for {_symbol}: {historicalData.Count} records");

            var data = historicalData.Select(c => new AnalysisMarketCandle() { Close = c.Close, Open = c.Open, High = c.High, Low = c.Low, TimeUtc = c.TimeUtc });

            var indicatorResults = _indicatorCalculationService.CalculateIndicators(data, 10);

            Console.WriteLine($"RSI: {indicatorResults.Rsi:F2}");
            Console.WriteLine($"EMA: {indicatorResults.Ema:F2}");
            Console.WriteLine($"SMA: {indicatorResults.Sma:F2}");
            Console.WriteLine($"ATR: {indicatorResults.Atr:F2}");
            Console.WriteLine($"MACD: {indicatorResults.Macd:F2}");


            var instrumentInfo = new InstrumentInfo(_symbol, indicatorResults);

            var result = await _aiRecommendationService.GetRecommendationAsync(instrumentInfo);

        }

    }
}
