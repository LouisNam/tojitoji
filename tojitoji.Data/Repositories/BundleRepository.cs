using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IBundleRepository : IRepository<Bundle>
    {
    }

    public class BundleRepository : RepositoryBase<Bundle>, IBundleRepository
    {
        public BundleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}