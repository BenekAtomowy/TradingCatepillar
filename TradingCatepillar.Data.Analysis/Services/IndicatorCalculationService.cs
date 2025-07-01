using TradingCatepillar.Data.Analysis.Models;
using TradingCatepillar.Data.Analysis.Services.Interfaces;

namespace TradingCatepillar.Data.Analysis.Services
{
    public class IndicatorCalculationService : IIndicatorCalculationService
    {

        public IndicatorResult CalculateIndicators(IEnumerable<AnalysisMarketCandle> candles, int period)
        {
            var candleList = candles.OrderBy(c => c.TimeUtc).ToList();
            if (candleList.Count < period + 1)
                throw new ArgumentException($"Min {period + 1} candles required");

            var closes = candleList.Select(c => c.Close).ToList();

            var ema = CalculateEma(closes, period);
            var sma = closes.Skip(closes.Count - period).Average();
            var rsi = CalculateRsi(closes, period);
            var atr = CalculateAtr(candleList, period);
            var macd = CalculateMacd(closes);

            return new IndicatorResult
            {
                Rsi = rsi,
                Ema = ema,
                Sma = sma,
                Atr = atr,
                Macd = macd
            };
        }

        /// <summary>
        /// Calculates the Exponential Moving Average (EMA) for a given list of closing prices.
        /// </summary>
        /// <param name="closes">A list of closing prices.</param>
        /// <param name="period">The number of periods over which to calculate the EMA.</param>
        /// <returns>The calculated EMA value.</returns>
        private decimal CalculateEma(List<decimal> closes, int period)
        {
            var k = 2m / (period + 1);
            decimal ema = closes.Take(period).Average();
            for (int i = period; i < closes.Count; i++)
                ema = closes[i] * k + ema * (1 - k);

            return ema;
        }

        /// <summary>
        /// Calculates the Relative Strength Index (RSI) for a given list of closing prices.
        /// </summary>
        /// <param name="closes">A list of closing prices.</param>
        /// <param name="period">The number of periods over which to calculate the RSI.</param>
        /// <returns>The calculated RSI value, which ranges from 0 to 100.</returns>
        private decimal CalculateRsi(List<decimal> closes, int period)
        {
            decimal gain = 0, loss = 0;
            for (int i = closes.Count - period; i < closes.Count; i++)
            {
                var diff = closes[i] - closes[i - 1];
                if (diff >= 0) gain += diff;
                else loss -= diff;
            }

            if (gain + loss == 0) return 50;

            var rs = gain / loss;
            return 100 - (100 / (1 + rs));
        }


        /// <summary>
        /// Calculates the Average True Range (ATR) for a given list of market candles.
        /// The ATR is a measure of market volatility, calculated as the average of the true ranges over a specified period.
        /// </summary>
        /// <param name="candles">A list of market candles containing high, low, and close prices.</param>
        /// <param name="period">The number of periods over which to calculate the ATR.</param>
        /// <returns>The calculated ATR value.</returns>
        private decimal CalculateAtr(List<AnalysisMarketCandle> candles, int period)
        {
            var trs = new List<decimal>();
            for (int i = candles.Count - period; i < candles.Count; i++)
            {
                var highLow = candles[i].High - candles[i].Low;
                var highClose = Math.Abs(candles[i].High - candles[i - 1].Close);
                var lowClose = Math.Abs(candles[i].Low - candles[i - 1].Close);
                trs.Add(Math.Max(highLow, Math.Max(highClose, lowClose)));
            }

            return trs.Average();
        }

        /// <summary>
        /// Calculates the Moving Average Convergence Divergence (MACD) for a given list of closing prices.
        /// The MACD is a trend-following momentum indicator that shows the relationship between two moving averages of a security's price.
        /// </summary>
        /// <param name="closes">A list of closing prices.</param>
        /// <returns>The calculated MACD value, which is the difference between the 12-period and 26-period EMA.</returns>
        private decimal CalculateMacd(List<decimal> closes)
        {
            var ema12 = CalculateEma(closes, 12);
            var ema26 = CalculateEma(closes, 26);
            return ema12 - ema26;
        }
    }
}
