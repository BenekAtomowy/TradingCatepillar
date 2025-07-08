
namespace TradingCatepillar.Core.Services.Interafaces
{
    public interface IInstrumentWorkerService
    {
        void AddWorker(string symbol);
        Task WorkAllAsync();
    }
}
