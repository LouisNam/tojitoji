using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface ICompanyInformationRepository : IRepository<CompanyInformation>
    {
    }

    public class CompanyInformationRepository : RepositoryBase<CompanyInformation>, ICompanyInformationRepository
    {
        public CompanyInformationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}