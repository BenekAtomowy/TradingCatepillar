using System.Text.Json;
using TradingCatepillar.Core.Builders;
using TradingCatepillar.Core.Builders.Interfaces;
using TradingCatepillar.Core.Models;
using TradingCatepillar.Core.Services.Interafaces;
using TradingCatepillar.Data.Analysis.Models;
using TradingCatepillar.Integration.GoogleGemini.Models;
using TradingCatepillar.Integration.GoogleGemini.Services.Interfaces;

namespace TradingCatepillar.Core.Services
{
    public class AIRecommendationService : IAIRecommendationService
    {
        private readonly IGeminiHttpClient _geminiHttpClient;
        private readonly IAIPromptBuilder _promptBuilder;
        public AIRecommendationService(IGeminiHttpClient geminiHttpClient, IAIPromptBuilder aIPromptBuilder) 
        { 
            _geminiHttpClient = geminiHttpClient ?? throw new ArgumentNullException(nameof(geminiHttpClient), "Gemini HTTP client cannot be null.");
            _promptBuilder = aIPromptBuilder ?? throw new ArgumentNullException(nameof(aIPromptBuilder), "AI prompt builder cannot be null.");
        }

        public async Task<InstrumentRecommendation> GetRecommendationAsync(InstrumentInfo instrumentInfo)
        {
            var instrumentPrompt = _promptBuilder.BuildPrompt(instrumentInfo);

            if (instrumentPrompt == null)
            {
                throw new ArgumentNullException(nameof(instrumentPrompt), "Instrument prompt cannot be null.");
            }
            var result = await _geminiHttpClient.Ask(instrumentPrompt);

            if (result == null)
            {
                throw new InvalidOperationException("Failed to get recommendation from Gemini.");
            }

            var recommendation = ParseAIAnswer<InstrumentRecommendation>(result);
            if (recommendation == null)
            {
                throw new InvalidOperationException("Failed to parse Gemini response into AIRecommendation.");
            }

            return recommendation;
        }


        private static T ParseAIAnswer<T>(AIAnswer aiAnswer) where T : AIAnswer
        {
            var json = aiAnswer.Answer;

            try
            {
                var result = JsonSerializer.Deserialize<T>(json);

                if (result == null)
                {
                    throw new InvalidOperationException("Deserialized result is null.");
                }

                return result;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing AI answer: {ex.Message}");
                throw new InvalidOperationException("Failed to parse AI answer.", ex);
            }
        }
    }
}
