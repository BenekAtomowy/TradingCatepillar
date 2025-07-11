using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TradingCatepillar.Persistence.Models
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }

        public RecommendationType RecommendationType { get; set; }
        
        public string? Risk { get; set; }
        
        public string? RiskPercent { get; set; }
        
        public string? Comment { get; set; }
        
        public string? Summary { get; set; }

    }
}
