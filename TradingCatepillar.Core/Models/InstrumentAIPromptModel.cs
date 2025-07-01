using TradingCatepillar.Data.Analysis.Models;
using TradingCatepillar.Integration.GoogleGemini.Models;

namespace TradingCatepillar.Core.Models
{
    public class InstrumentAIPromptModel : AIPrompt
    {
        public InstrumentAIPromptModel(string symbol, IndicatorResult indicatorResult)
        {
            InstrumentIndicators = indicatorResult;
            InstrumentSymbol = symbol;
            SystemInformation = "You are a financial assistant who always responds in JSON.";
            PromptInformation = @$"Find information on the internet about the financial instrument with the symbol {symbol}                                                                 
                            The indicators I calculated:
                            RSI: {InstrumentIndicators.Rsi:F2}
                            EMA: {InstrumentIndicators.Ema:F2}
                            SMA: {InstrumentIndicators.Sma:F2}
                            ATR: {InstrumentIndicators.Atr:F2}
                            MACD: {InstrumentIndicators.Macd:F2}";
            OutputDataFormat = @"Based on the information found and the indicators I calculated, evaluate the financial instrument and return EXCLUSIVELY JSON in the following format:
            {
                {
                    'recommendation': 'BUY | SELL | HOLD',
                  'risk': 'LOW | MEDIUM | HIGH',
                  'riskPercent': 'percentage of risk',
                  'comment': '...',
                  'links': 'links to source news that this recommendation is based'
                }
            }

            Do not add any other text or explanations before or after the JSON.";

        }

        public string InstrumentSymbol { get; private set; } = string.Empty;
        public IndicatorResult InstrumentIndicators { get; private set; }


    }
}
