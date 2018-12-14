using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ISKURepository : IRepository<SKU>
    {
    }

    public class SKURepository : RepositoryBase<SKU>, ISKURepository
    {
        public SKURepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}