//using TradingCatepillar.Services;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace TradingCatepillar.Simulator
//{
//    public class Simulation
//    {
//        private DateTime _start;
//        private DateTime _end;
//        private readonly XTBClient _client;
//        private double _balance = 10000;
//        private double _volume = 1;
        
//        private Position _position;
//        private string _symbol;

//        public Simulation(DateTime start, DateTime end, string symbol) 
//        {
//            _client = new XTBClient();
//            _start = start;
//            _end = end;
//            _symbol = symbol;

//        }

//        public async Task Run()
//        {
//            var calendar = _client.GetChartRange(_start, _end, _symbol);
//            var rateList = calendar.RateInfos.ToList();

//            for (var i = 3; i < rateList.Count; i++)
//            {
//                var rate = rateList[i];
//                var previousRate = rateList[i - 1];
//                var previousRate2 = rateList[i - 2];
//                var previousRate3 = rateList[i - 3];

//                if (_position is null && rate.Open > previousRate.Open)
//                {
//                    //buy
//                    Console.WriteLine($"Buy position for: {rate.Open}");
//                    _position = new Position()
//                    {
//                        OpenRate = rate.Open.Value,
//                        Volume = _volume
//                    };
//                    _balance = _balance - _position.OpenRate * _position.Volume;

//                }
//                else if (_position is not null && rate.Close > 0 && previousRate.Close > rate.Close)
//                {
//                    //sell
//                    Console.WriteLine($"Sell position for: {rate.Open.Value + rate.Close.Value}");

//                    _balance = _balance + (rate.Open.Value + rate.Close.Value) * _position.Volume;
//                    _position = null;
//                }
//            }
//            var balance = _position is null ? _balance : _balance + (rateList.Last().Open + rateList.Last().Close) * _position.Volume;
//            Console.WriteLine($"Balance: {balance}");
//        }

//        public class Position
//        {
//            public double Volume { get; set; }  
//            public double OpenRate { get; set; }
//        }

//    }
//}
