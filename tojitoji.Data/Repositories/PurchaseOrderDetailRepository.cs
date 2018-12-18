using System.Data.SqlClient;
using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IPurchaseOrderDetailRepository : IRepository<PurchaseOrderDetail>
    {
        void CreatePurchaseOrderDetail(int productID, int purchaseOrderID, decimal price, int quantity);
    }

    public class PurchaseOrderDetailRepository : RepositoryBase<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        public PurchaseOrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void CreatePurchaseOrderDetail(int productID, int purchaseOrderID, decimal price, int quantity)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", productID),
                new SqlParameter("@PurchaseOrderID", purchaseOrderID),
                new SqlParameter("@PurchasingPrice", price),
                new SqlParameter("@Quantity", quantity)
            };

            DbContext.Database.SqlQuery<object>("CreatePurchaseOrderDetail", parameters);
        }
    }
}