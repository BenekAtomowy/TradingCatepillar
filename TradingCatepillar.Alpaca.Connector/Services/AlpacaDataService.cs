using Alpaca.Markets;
using TradingCatepillar.Alpaca.Connector.Models;
using TradingCatepillar.Alpaca.Connector.Services.Interfaces;
using TradingCatepillar.AlpacaAPI.Connector;

namespace TradingCatepillar.Alpaca.Connector.Services
{
    public class AlpacaDataService : IAlpacaDataService
    {
        private readonly IAlpacaDataClient _dataClient;

        public AlpacaDataService(IAlpacaDataClient dataClient)
        {
            _dataClient = dataClient;
        }

        public async Task<IReadOnlyList<MarketCandle>> GetHistoricalDataAsync(string symbol, TimeSpan interval, DateTime fromUtc, DateTime toUtc)
        {
            var bars = await _dataClient.ListHistoricalBarsAsync(new HistoricalBarsRequest(symbol, fromUtc, toUtc, GetTimeFrame(interval)));

            return bars.Items.Select(bar => new MarketCandle
            {
                TimeUtc = bar.TimeUtc,
                Open = bar.Open,
                High = bar.High,
                Low = bar.Low,
                Close = bar.Close,
                Volume = bar.Volume
            }).ToList();
        }

        public async Task<InstrumentSnapshot> GetCurrentSnapshotAsync(string symbol)
        {
            var snapshot = await _dataClient.GetSnapshotAsync(new (symbol));

            return new InstrumentSnapshot
            {
                Symbol = symbol,
                LastUpdateUtc = snapshot.Trade?.TimestampUtc ?? DateTime.UtcNow,
                LastPrice = snapshot.Trade?.Price ?? 0,
                BidPrice = snapshot.Quote?.BidPrice ?? 0,
                AskPrice = snapshot.Quote?.AskPrice ?? 0,
                DailyOpen = snapshot.CurrentDailyBar?.Open ?? 0,
                DailyHigh = snapshot.CurrentDailyBar?.High ?? 0,
                DailyLow = snapshot.CurrentDailyBar?.Low ?? 0,
                DailyVolume = snapshot.CurrentDailyBar?.Volume ?? 0,
                ChangePercent = CalculateChangePercent(snapshot.CurrentDailyBar, snapshot.PreviousDailyBar)
            };
        }

        private decimal CalculateChangePercent(IBar? current, IBar? previous)
        {
            if (current == null || previous == null || previous.Close == 0)
                return 0;

            return (current.Close - previous.Close) / previous.Close * 100;
        }

        /// <summary>
        /// Gets the appropriate BarTimeFrame based on the specified time interval.
        /// </summary>
        /// <param name="interval">The time interval as a TimeSpan.</param>
        /// <returns>A BarTimeFrame corresponding to the given interval.</returns>
        private static BarTimeFrame GetTimeFrame(TimeSpan interval)
        {
            return interval.TotalMinutes switch
            {
                <= 1 => BarTimeFrame.Minute,
                <= 5 => new BarTimeFrame(5, BarTimeFrameUnit.Minute),
                <= 15 => new BarTimeFrame(15, BarTimeFrameUnit.Minute),
                <= 60 => BarTimeFrame.Hour,
                _ => BarTimeFrame.Day
            };
        }

        /// <summary>
        /// Test method 
        /// </summary>
        /// <returns></returns>
        public async Task Process()
        {
            string symbol = "USO";
            TimeSpan timeframe = TimeSpan.FromHours(1);
            int historicalBarsCount = 100;

            DateTime endTime = DateTime.UtcNow.AddDays(-1);
            DateTime startTime = endTime.AddDays(-7);

            var data = await GetHistoricalDataAsync(symbol, timeframe, startTime, endTime);


            var snapshot = await GetCurrentSnapshotAsync("MSFT");
        }
    }
}
