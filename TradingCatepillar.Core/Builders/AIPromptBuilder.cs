using TradingCatepillar.Core.Builders.Interfaces;
using TradingCatepillar.Core.Models;
using TradingCatepillar.Integration.GoogleGemini.Models;

namespace TradingCatepillar.Core.Builders
{
    public class AIPromptBuilder : IAIPromptBuilder
    {
        public AIPrompt BuildPrompt(InstrumentInfo instrumentPrompt)
        {
            var prompt = new AIPrompt();
            prompt.SystemInformation = "You are a financial assistant who always responds in JSON.";
            prompt.PromptInformation = @$"Find latest helpful for analysis information on the internet about the financial instrument with the symbol {instrumentPrompt.InstrumentSymbol} and deeply research it.                                                                
                            The indicators I calculated:
                            RSI: {instrumentPrompt.InstrumentIndicators.Rsi:F2}
                            EMA: {instrumentPrompt.InstrumentIndicators.Ema:F2}
                            SMA: {instrumentPrompt.InstrumentIndicators.Sma:F2}
                            ATR: {instrumentPrompt.InstrumentIndicators.Atr:F2}
                            MACD: {instrumentPrompt.InstrumentIndicators.Macd:F2}";
            prompt.OutputDataFormat = @"Based on the information found and the indicators I calculated, evaluate the financial instrument and return EXCLUSIVELY a single JSON object in the following format (do NOT return an array):
            {
              'recommendation': 'BUY | SELL | HOLD',
              'risk': 'LOW | MEDIUM | HIGH',
              'riskPercent': 'percentage of risk',
              'comment': 'comment based on indicators',
              'summary': 'summary of information from research in internet',
              'links': 'links to source news that this recommendation is based'
            }

            Do not add any other text or explanations before or after the JSON.";

            return prompt;

        }
    }
}
