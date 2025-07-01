using Alpaca.Markets;
using TradingCatepillar.Alpaca.Connector.Services.Interfaces;

namespace TradingCatepillar.Alpaca.Connector.Services
{
    /// <summary>
    /// Moduł wykonawczy – odpowiedzialny za wysyłanie zleceń kupna i sprzedaży.
    /// </summary>
    public class AlpacaTradeService : IAlpacaTradeService
    {
        private readonly IAlpacaTradingClient _tradingClient;
        public AlpacaTradeService(IAlpacaTradingClient tradingClient)
        {
            _tradingClient = tradingClient;
        }

        public async Task BuyAsync(string symbol, int quantity, decimal limitPrice)
        {
            try
            {
                var order = await _tradingClient.PostOrderAsync(
                    OrderSide.Buy.Market(symbol, quantity));
                Console.WriteLine($"Buy order sent for {symbol}, OrderId: {order.OrderId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error buying {symbol}: {ex.Message}");
            }
        }

        public async Task SellAsync(string symbol, decimal limitPrice)
        {
            try
            {
                var order = await _tradingClient.PostOrderAsync(
                    OrderSide.Sell.Market(symbol, 1));
                Console.WriteLine($"Sell order sent for {symbol}, OrderId: {order.OrderId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selling {symbol}: {ex.Message}");
            }
        }

        public async Task CreateOrder()
        {
            //orders
            var orders = await _tradingClient.ListOrdersAsync(new ListOrdersRequest());

            var order = await _tradingClient.PostOrderAsync(new NewOrderRequest(
                symbol: "MSFT", OrderQuantity.FromInt64(1), OrderSide.Buy, OrderType.Market, TimeInForce.Day)
            {
                TakeProfitLimitPrice = 310.00m, // Target price for profit
                StopLossLimitPrice = 290.00m   // Price to limit losses
            });

            orders = await _tradingClient.ListOrdersAsync(new ListOrdersRequest());
        }

    }
}

