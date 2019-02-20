using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ICoSoKinhDoanhRepository : IRepository<CoSoKinhDoanh>
    {
    }

    public class CoSoKinhDoanhRepository : RepositoryBase<CoSoKinhDoanh>, ICoSoKinhDoanhRepository
    {
        public CoSoKinhDoanhRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}