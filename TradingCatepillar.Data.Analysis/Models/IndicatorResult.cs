namespace TradingCatepillar.Data.Analysis.Models
{
    /// <summary>
    /// Represents the result of various technical indicators.
    /// </summary>
    public class IndicatorResult
    {
        /// <summary>
        /// Gets or sets the Relative Strength Index (RSI).
        /// </summary>
        public decimal? Rsi { get; set; }

        /// <summary>
        /// Gets or sets the Exponential Moving Average (EMA).
        /// </summary>
        public decimal? Ema { get; set; }

        /// <summary>
        /// Gets or sets the Simple Moving Average (SMA).
        /// </summary>
        public decimal? Sma { get; set; }

        /// <summary>
        /// Gets or sets the Average True Range (ATR).
        /// </summary>
        public decimal? Atr { get; set; }

        /// <summary>
        /// Gets or sets the Moving Average Convergence Divergence (MACD).
        /// </summary>
        public decimal? Macd { get; set; }
    }
}
