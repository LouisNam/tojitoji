using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
    }

    public class WarehouseRepository : RepositoryBase<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}