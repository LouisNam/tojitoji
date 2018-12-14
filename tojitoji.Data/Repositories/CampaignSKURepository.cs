using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ICampaignSKURepository : IRepository<CampaignSKU>
    {
    }

    public class CampaignSKURepository : RepositoryBase<CampaignSKU>, ICampaignSKURepository
    {
        public CampaignSKURepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}