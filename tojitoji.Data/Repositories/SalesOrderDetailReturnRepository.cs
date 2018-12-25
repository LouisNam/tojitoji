using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ISalesOrderDetailReturnRepository : IRepository<SalesOrderDetailReturn>
    {
    }

    public class SalesOrderDetailReturnRepository : RepositoryBase<SalesOrderDetailReturn>, ISalesOrderDetailReturnRepository
    {
        public SalesOrderDetailReturnRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}