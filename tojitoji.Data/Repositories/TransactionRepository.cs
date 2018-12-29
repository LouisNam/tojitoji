using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
    }

    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}