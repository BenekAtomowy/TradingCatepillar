using AutoMapper;
using System.Net.NetworkInformation;
using TradingCatepillar.Alpaca.Connector.Services.Interfaces;
using TradingCatepillar.Core.Builders.Interfaces;
using TradingCatepillar.Core.Services.Interafaces;
using TradingCatepillar.Core.Workers;
using TradingCatepillar.Data.Analysis.Services.Interfaces;
using TradingCatepillar.Persistence.Models;
using TradingCatepillar.Persistence.Repositories.Interfaces;

namespace TradingCatepillar.Core.Builders
{
    public class InstrumentWorkerBuilder : IInstrumentWorkerBuilder
    {
        private readonly IAlpacaDataService _alpacaDataService;
        private readonly IIndicatorCalculationService _indicatorCalculationService;
        private readonly IAIRecommendationService _aiRecommendationService;
        private readonly IMapper _mapper;
        private readonly IRepository<Recommendation> _recommendationRepository;

        public InstrumentWorkerBuilder(IAlpacaDataService alpacaDataService, IIndicatorCalculationService indicatorCalculationService, IAIRecommendationService aiRecommendationService, IRepository<Recommendation> recommendationRepository, IMapper mapper)
        {
            _alpacaDataService = alpacaDataService;
            _indicatorCalculationService = indicatorCalculationService;
            _aiRecommendationService = aiRecommendationService;
            _recommendationRepository = recommendationRepository;
            _mapper = mapper;
        }

        public InstrumentWorker BuildWorker(string symbol)
        {
            var worker = new InstrumentWorker(_alpacaDataService, _indicatorCalculationService, _aiRecommendationService, _mapper, _recommendationRepository);
            worker.Initialize(symbol);
            return worker;
        }
    }
}
