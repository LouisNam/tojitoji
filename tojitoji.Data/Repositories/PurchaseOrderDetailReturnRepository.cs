using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IPurchaseOrderDetailReturnRepository : IRepository<PurchaseOrderDetailReturn>
    {
    }

    public class PurchaseOrderDetailReturnRepository : RepositoryBase<PurchaseOrderDetailReturn>, IPurchaseOrderDetailReturnRepository
    {
        public PurchaseOrderDetailReturnRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}