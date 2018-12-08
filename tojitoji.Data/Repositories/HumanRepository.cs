using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IHumanRepository : IRepository<Human>
    {
    }

    public class HumanRepository : RepositoryBase<Human>, IHumanRepository
    {
        public HumanRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}