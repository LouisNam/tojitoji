using tojitoji.Data.Infrastructure;
using tojitoji.Model.Models;

namespace tojitoji.Data.Repositories
{
    public interface IDocumentRepository : IRepository<Document>
    {
    }

    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}