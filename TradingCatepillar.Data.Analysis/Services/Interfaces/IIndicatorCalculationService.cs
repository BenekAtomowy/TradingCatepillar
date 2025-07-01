using TradingCatepillar.Data.Analysis.Models;

namespace TradingCatepillar.Data.Analysis.Services.Interfaces
{
    /// <summary>
    /// Defines a service for calculating trading indicators based on market candles.
    /// </summary>
    public interface IIndicatorCalculationService
    {
        /// <summary>
        /// Calculates the indicators for the specified market candles over a given period.
        /// </summary>
        /// <param name="candles">A collection of market candles to analyze.</param>
        /// <param name="period">The period over which to calculate the indicators. Period for indicators (ex. 14 for RSI, EMA)</param>
        /// <returns>An <see cref="IndicatorResult"/> containing the calculated indicators.</returns>
        IndicatorResult CalculateIndicators(IEnumerable<AnalysisMarketCandle> candles, int period);
    }
}
