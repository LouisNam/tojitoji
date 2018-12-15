using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
    }

    public class PurchaseOrderRepository : RepositoryBase<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}