using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IDocumentService
    {
        IEnumerable<Document> GetAll();

        Document GetById(int id);

        Document Add(Document Document);

        void Update(Document Document);

        Document Delete(int id);

        void SaveChanges();
    }

    public class DocumentService : IDocumentService
    {
        private IDocumentRepository _documentRepository;
        private IUnitOfWork _unitOfWork;

        public DocumentService(IDocumentRepository documentRepository, IUnitOfWork unitOfWork)
        {
            this._documentRepository = documentRepository;
            this._unitOfWork = unitOfWork;
        }

        public Document Add(Document document)
        {
            return _documentRepository.Add(document);
        }

        public Document Delete(int id)
        {
            return _documentRepository.Delete(id);
        }

        public IEnumerable<Document> GetAll()
        {
            return _documentRepository.GetAll();
        }

        public Document GetById(int id)
        {
            return _documentRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Document document)
        {
            _documentRepository.Update(document);
        }
    }
}