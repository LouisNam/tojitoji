using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ITimeTrichKhauHaoTSCDRepository : IRepository<TimeTrichKhauHaoTSCD>
    {
    }

    public class TimeTrichKhauHaoTSCDRepository : RepositoryBase<TimeTrichKhauHaoTSCD>, ITimeTrichKhauHaoTSCDRepository
    {
        public TimeTrichKhauHaoTSCDRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}