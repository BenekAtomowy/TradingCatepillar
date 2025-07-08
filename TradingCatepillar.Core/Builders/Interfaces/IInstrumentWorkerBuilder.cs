using TradingCatepillar.Core.Workers;

namespace TradingCatepillar.Core.Builders.Interfaces
{
    public interface IInstrumentWorkerBuilder
    {
        InstrumentWorker BuildWorker(string symbol);
    }
}
