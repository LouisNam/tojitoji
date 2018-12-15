using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IPurchaseOrderDetailRepository : IRepository<PurchaseOrderDetail>
    {
    }

    public class PurchaseOrderDetailRepository : RepositoryBase<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        public PurchaseOrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}