using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IInventoryTransactionRepository : IRepository<InventoryTransaction>
    {
    }

    public class InventoryTransactionRepository : RepositoryBase<InventoryTransaction>, IInventoryTransactionRepository
    {
        public InventoryTransactionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}