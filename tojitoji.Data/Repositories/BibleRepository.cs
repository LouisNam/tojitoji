using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IBibleRepository : IRepository<Bible>
    {
    }

    public class BibleRepository : RepositoryBase<Bible>, IBibleRepository
    {
        public BibleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}