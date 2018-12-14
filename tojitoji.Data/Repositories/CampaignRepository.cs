using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
    }

    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {
        public CampaignRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}