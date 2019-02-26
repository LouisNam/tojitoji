using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ILoaiKhoRepository : IRepository<LoaiKho>
    {
    }

    public class LoaiKhoRepository : RepositoryBase<LoaiKho>, ILoaiKhoRepository
    {
        public LoaiKhoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}