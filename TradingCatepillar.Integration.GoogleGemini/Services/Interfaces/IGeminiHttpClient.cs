
using TradingCatepillar.Integration.GoogleGemini.Models;

namespace TradingCatepillar.Integration.GoogleGemini.Services.Interfaces
{
    public interface IGeminiHttpClient
    {
        Task<AIRecommendation> AskForRecommendation(AIPrompt instrumentPrompt);
    }
}
