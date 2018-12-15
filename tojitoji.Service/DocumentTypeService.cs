using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IDocumentTypeService
    {
        DocumentType Add(DocumentType documentType);

        void Update(DocumentType documentType);

        DocumentType Delete(int id);

        IEnumerable<DocumentType> GetAll();

        DocumentType GetById(string id);

        void SaveChanges();
    }

    public class DocumentTypeService : IDocumentTypeService
    {
        private IDocumentTypeRepository _documentTypeRepository;
        private IUnitOfWork _unitOfWork;

        public DocumentTypeService(IDocumentTypeRepository documentTypeRepository, IUnitOfWork unitOfWork)
        {
            this._documentTypeRepository = documentTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public DocumentType Add(DocumentType documentType)
        {
            return _documentTypeRepository.Add(documentType);
        }

        public DocumentType Delete(int id)
        {
            return _documentTypeRepository.Delete(id);
        }

        public IEnumerable<DocumentType> GetAll()
        {
            return _documentTypeRepository.GetAll();
        }

        public DocumentType GetById(string id)
        {
            return _documentTypeRepository.GetSingleByCondition(x => x.ID == id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(DocumentType documentType)
        {
            _documentTypeRepository.Update(documentType);
        }
    }
}