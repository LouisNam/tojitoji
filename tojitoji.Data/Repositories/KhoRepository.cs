using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IKhoRepository : IRepository<Kho>
    {
    }

    public class KhoRepository : RepositoryBase<Kho>, IKhoRepository
    {
        public KhoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}