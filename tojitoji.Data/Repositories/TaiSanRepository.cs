using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ITaiSanRepository : IRepository<TaiSan>
    {
    }

    public class TaiSanRepository : RepositoryBase<TaiSan>, ITaiSanRepository
    {
        public TaiSanRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}