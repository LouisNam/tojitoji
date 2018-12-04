using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
    }

    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}