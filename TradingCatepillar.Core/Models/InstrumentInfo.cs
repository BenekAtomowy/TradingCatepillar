using TradingCatepillar.Data.Analysis.Models;
using TradingCatepillar.Integration.GoogleGemini.Models;

namespace TradingCatepillar.Core.Models
{
    public class InstrumentInfo
    {
        public InstrumentInfo(string symbol, IndicatorResult indicatorResult)
        {
            InstrumentIndicators = indicatorResult;
            InstrumentSymbol = symbol;
        }

        public string InstrumentSymbol { get; private set; } = string.Empty;
        public IndicatorResult InstrumentIndicators { get; private set; }


    }
}
