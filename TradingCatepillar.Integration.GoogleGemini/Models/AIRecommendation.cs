using System.Text.Json.Serialization;

namespace TradingCatepillar.Integration.GoogleGemini.Models
{
    public record AIRecommendation
    {
        [JsonPropertyName("recommendation")]
        public string? Recommendation { get; set; }
        [JsonPropertyName("risk")]
        public string? Risk { get; set; }
        [JsonPropertyName("riskPercent")]
        public string? RiskPercent { get; set; }
        [JsonPropertyName("comment")]
        public string? Comment { get; set; } 

    }
}
