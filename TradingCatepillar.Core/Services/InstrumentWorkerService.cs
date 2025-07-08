using TradingCatepillar.Core.Builders.Interfaces;
using TradingCatepillar.Core.Services.Interafaces;
using TradingCatepillar.Core.Workers;

namespace TradingCatepillar.Core.Services
{
    public class InstrumentWorkerService : IInstrumentWorkerService
    {
        private readonly IInstrumentWorkerBuilder _workerBuilder;
        private readonly List<InstrumentWorker> _workers = new();

        public InstrumentWorkerService(IInstrumentWorkerBuilder workerBuilder)
        {
            _workerBuilder = workerBuilder;
        }

        public void AddWorker(string symbol)
        {
            var worker = _workerBuilder.BuildWorker(symbol);
            _workers.Add(worker);
        }

        public async Task WorkAllAsync()
        {
            while (true)
            {
                try
                {
                    var tasks = new List<Task>();
                    foreach (var worker in _workers)
                    {
                        tasks.Add(worker.WorkWithInstrument());
                    }
                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during instrument processing: {ex.Message}");
                }
                // Wait for a while before the next iteration
                await Task.Delay(TimeSpan.FromSeconds(20));
            }
           
        }

    }
}
