using TradingCatepillar.Persistence.Models;

namespace TradingCatepillar.Persistence.Repositories
{
    public class RecommendationRepository : BaseRepository<Recommendation>
    {
        public RecommendationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task AddAsync(Recommendation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Recommendation entity cannot be null.");
            }
            entity.CreateTime = DateTime.Now;
            await base.AddAsync(entity);
        }


    }
}
