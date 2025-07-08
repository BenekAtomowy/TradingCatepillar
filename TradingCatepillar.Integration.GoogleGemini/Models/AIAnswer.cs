namespace TradingCatepillar.Integration.GoogleGemini.Models
{
    public class AIAnswer
    {
        public AIAnswer(string answer)
        {
            Answer = answer;
        }
        public string Answer { get; set; }
    }
}
