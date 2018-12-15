using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IDocumentTypeRepository : IRepository<DocumentType>
    {
    }

    public class DocumentTypeRepository : RepositoryBase<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}