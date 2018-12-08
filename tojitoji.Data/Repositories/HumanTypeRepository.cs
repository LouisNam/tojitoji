using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IHumanTypeRepository : IRepository<HumanType>
    {
    }

    public class HumanTypeRepository : RepositoryBase<HumanType>, IHumanTypeRepository
    {
        public HumanTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}