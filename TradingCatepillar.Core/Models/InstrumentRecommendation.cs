using System.Text.Json.Serialization;

namespace TradingCatepillar.Integration.GoogleGemini.Models
{
    public class InstrumentRecommendation : AIAnswer
    {
        
        public InstrumentRecommendation(string answer) : base(answer)
        {
        }

        [JsonPropertyName("recommendation")]
        public string? Recommendation { get; set; }
        [JsonPropertyName("risk")]
        public string? Risk { get; set; }
        [JsonPropertyName("riskPercent")]
        public string? RiskPercent { get; set; }
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }
        [JsonPropertyName("summary")]
        public string? Summary { get; set; }
        [JsonPropertyName("links")]
        public List<string>? Links { get; set; }

    }
}
