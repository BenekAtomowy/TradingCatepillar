using TradingCatepillar.Core.Models;
using TradingCatepillar.Integration.GoogleGemini.Models;

namespace TradingCatepillar.Core.Builders.Interfaces
{
    public interface IAIPromptBuilder
    {
        AIPrompt BuildPrompt(InstrumentInfo instrumentPrompt);
    }
}
