namespace TradingCatepillar.Alpaca.Connector.Models
{
    /// <summary>
    /// Represents a snapshot of an instrument's market data.
    /// </summary>
    public class InstrumentSnapshot
    {
        /// <summary>
        /// Gets or sets the symbol of the instrument.
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last update time in UTC.
        /// </summary>
        public DateTime LastUpdateUtc { get; set; }

        /// <summary>
        /// Gets or sets the last traded price of the instrument.
        /// </summary>
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Gets or sets the current bid price for the instrument.
        /// </summary>
        public decimal BidPrice { get; set; }

        /// <summary>
        /// Gets or sets the current ask price for the instrument.
        /// </summary>
        public decimal AskPrice { get; set; }

        /// <summary>
        /// Gets or sets the daily opening price of the instrument.
        /// </summary>
        public decimal DailyOpen { get; set; }

        /// <summary>
        /// Gets or sets the daily highest price of the instrument.
        /// </summary>
        public decimal DailyHigh { get; set; }

        /// <summary>
        /// Gets or sets the daily lowest price of the instrument.
        /// </summary>
        public decimal DailyLow { get; set; }

        /// <summary>
        /// Gets or sets the daily trading volume of the instrument.
        /// </summary>
        public decimal DailyVolume { get; set; }

        /// <summary>
        /// Gets or sets the percentage change in price for the day.
        /// </summary>
        public decimal ChangePercent { get; set; }
    }

}
