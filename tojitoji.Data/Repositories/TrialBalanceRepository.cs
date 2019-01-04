using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ITrialBalanceRepository : IRepository<TrialBalance>
    {
    }

    public class TrialBalanceRepository : RepositoryBase<TrialBalance>, ITrialBalanceRepository
    {
        public TrialBalanceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}