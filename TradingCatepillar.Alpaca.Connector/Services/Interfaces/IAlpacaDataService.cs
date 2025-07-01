
using TradingCatepillar.Alpaca.Connector.Models;

namespace TradingCatepillar.Alpaca.Connector.Services.Interfaces
{
    public interface IAlpacaDataService
    {
        /// <summary>
        /// Retrieves the current snapshot of the specified instrument.
        /// </summary>
        /// <param name="symbol">The symbol of the instrument.</param>
        /// <returns>A task that represents the asynchronous operation, containing the current instrument snapshot.</returns>
        Task<InstrumentSnapshot> GetCurrentSnapshotAsync(string symbol);

        /// <summary>
        /// Retrieves historical market data for the specified instrument.
        /// </summary>
        /// <param name="symbol">The symbol of the instrument.</param>
        /// <param name="interval">The time interval for the historical data.</param>
        /// <param name="fromUtc">The start date and time in UTC for the historical data.</param>
        /// <param name="toUtc">The end date and time in UTC for the historical data.</param>
        /// <returns>A task that represents the asynchronous operation, containing a read-only list of market candles.</returns>
        Task<IReadOnlyList<MarketCandle>> GetHistoricalDataAsync(string symbol, TimeSpan interval, DateTime fromUtc, DateTime toUtc);
    }
}
