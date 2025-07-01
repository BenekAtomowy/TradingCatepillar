
namespace TradingCatepillar.Alpaca.Connector.Services.Interfaces
{
    /// <summary>
    /// Interface for Alpaca trading services.
    /// </summary>
    public interface IAlpacaTradeService
    {
        /// <summary>
        /// Buys a specified quantity of a stock at a given limit price.
        /// </summary>
        /// <param name="symbol">The stock symbol to buy.</param>
        /// <param name="quantity">The number of shares to buy.</param>
        /// <param name="limitPrice">The maximum price per share to pay.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task BuyAsync(string symbol, int quantity, decimal limitPrice);

        /// <summary>
        /// Sells a stock at a given limit price.
        /// </summary>
        /// <param name="symbol">The stock symbol to sell.</param>
        /// <param name="limitPrice">The minimum price per share to receive.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SellAsync(string symbol, decimal limitPrice);
    }
}
