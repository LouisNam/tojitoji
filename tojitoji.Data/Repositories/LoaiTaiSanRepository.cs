using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ILoaiTaiSanRepository : IRepository<LoaiTaiSan>
    {
    }

    public class LoaiTaiSanRepository : RepositoryBase<LoaiTaiSan>, ILoaiTaiSanRepository
    {
        public LoaiTaiSanRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}