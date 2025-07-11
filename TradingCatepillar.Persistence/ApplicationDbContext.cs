using Microsoft.EntityFrameworkCore;
using TradingCatepillar.Persistence.Models;

namespace TradingCatepillar.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Recommendation> Recommendations { get; set; }

    }
}
