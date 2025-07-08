using Alpaca.Markets;
using Microsoft.Extensions.Configuration;

namespace TradingCatepillar.AlpacaAPI.Connector
{

    public class AlpacaAPIClient
    {
        private readonly string API_KEY;
        private readonly string API_SECRET;

        /// <summary>
        /// Client integrating the connection with Alpaca API and trading strategy.
        /// </summary>

        private IAlpacaTradingClient _tradingClient;
        private IAlpacaDataClient _dataClient;

        public IAlpacaDataClient DataClient { get { return _dataClient; } }
        public IAlpacaTradingClient TradingClient {  get { return _tradingClient; } }

        public AlpacaAPIClient(IConfiguration config)
        {
            API_KEY = config["ApiSettings:Alpaca:API_KEY"] ?? throw new NullReferenceException("ApiSettings:Alpaca:ApiKey");
            API_SECRET = config["ApiSettings:Alpaca:API_SECRET"] ?? throw new NullReferenceException("ApiSettings:Alpaca:API_SECRET");

            _tradingClient = Environments.Paper.GetAlpacaTradingClient(new SecretKey(API_KEY, API_SECRET));
            _dataClient = Environments.Paper.GetAlpacaDataClient(new SecretKey(API_KEY, API_SECRET));
        }

        public async Task Initialize()
        {
           
            // Initialization of Alpaca clients (Paper Trading mode)

            var account = await _tradingClient.GetAccountAsync();
            Console.WriteLine($"Account Equity: {account.Equity}, Last Equity: {account.LastEquity}");

            // Strategy settings - candle interval, number of historical candles, and polling interval
            TimeSpan timeframe = TimeSpan.FromHours(2);

        }
    }
}
