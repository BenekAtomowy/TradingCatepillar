using TradingCatepillar.Core.Models;
using TradingCatepillar.Integration.GoogleGemini.Models;

namespace TradingCatepillar.Core.Services.Interafaces
{
    public interface IAIRecommendationService
    {
        Task<InstrumentRecommendation> GetRecommendationAsync(InstrumentInfo instrumentInfo);
    }
}
