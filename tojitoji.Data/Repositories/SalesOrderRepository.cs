using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ISalesOrderRepository : IRepository<SalesOrder>
    {
    }

    public class SalesOrderRepository : RepositoryBase<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}