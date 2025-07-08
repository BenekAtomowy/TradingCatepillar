using TradingCatepillar.Alpaca.Connector.Services.Interfaces;
using TradingCatepillar.Core.Builders.Interfaces;
using TradingCatepillar.Core.Services.Interafaces;
using TradingCatepillar.Core.Workers;
using TradingCatepillar.Data.Analysis.Services.Interfaces;

namespace TradingCatepillar.Core.Builders
{
    public class InstrumentWorkerBuilder : IInstrumentWorkerBuilder
    {
        private readonly IAlpacaDataService _alpacaDataService;
        private readonly IIndicatorCalculationService _indicatorCalculationService;
        private readonly IAIRecommendationService _aiRecommendationService;

        public InstrumentWorkerBuilder(IAlpacaDataService alpacaDataService, IIndicatorCalculationService indicatorCalculationService, IAIRecommendationService aiRecommendationService)
        {
            _alpacaDataService = alpacaDataService;
            _indicatorCalculationService = indicatorCalculationService;
            _aiRecommendationService = aiRecommendationService;
        }

        public InstrumentWorker BuildWorker(string symbol)
        {
            var worker = new InstrumentWorker(_alpacaDataService, _indicatorCalculationService, _aiRecommendationService);
            worker.Initialize(symbol);
            return worker;
        }
    }
}
