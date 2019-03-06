using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ITangGiamTSCDRepository : IRepository<TangGiamTSCD>
    {
    }

    public class TangGiamTSCDRepository : RepositoryBase<TangGiamTSCD>, ITangGiamTSCDRepository
    {
        public TangGiamTSCDRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}