using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ISalesOrderDetailRepository : IRepository<SalesOrderDetail>
    {
    }

    public class SalesOrderDetailRepository : RepositoryBase<SalesOrderDetail>, ISalesOrderDetailRepository
    {
        public SalesOrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}