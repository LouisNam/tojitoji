using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ISKULazadaRepository : IRepository<SKULazada>
    {
    }

    public class SKULazadaRepository : RepositoryBase<SKULazada>, ISKULazadaRepository
    {
        public SKULazadaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}