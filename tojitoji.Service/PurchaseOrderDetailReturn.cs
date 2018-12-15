using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IPurchaseOrderDetailReturnService
    {
        PurchaseOrderDetailReturn Add(PurchaseOrderDetailReturn purchaseOrderDetailReturn);

        void Update(PurchaseOrderDetailReturn purchaseOrderDetailReturn);

        PurchaseOrderDetailReturn Delete(int id);

        IEnumerable<PurchaseOrderDetailReturn> GetAll();

        PurchaseOrderDetailReturn GetById(int id);

        void SaveChanges();
    }

    public class PurchaseOrderDetailReturnService : IPurchaseOrderDetailReturnService
    {
        private IPurchaseOrderDetailReturnRepository _purchaseOrderDetailReturnRepository;
        private IUnitOfWork _unitOfWork;

        public PurchaseOrderDetailReturnService(IPurchaseOrderDetailReturnRepository purchaseOrderDetailReturnRepository, IUnitOfWork unitOfWork)
        {
            this._purchaseOrderDetailReturnRepository = purchaseOrderDetailReturnRepository;
            this._unitOfWork = unitOfWork;
        }

        public PurchaseOrderDetailReturn Add(PurchaseOrderDetailReturn purchaseOrderDetailReturn)
        {
            return _purchaseOrderDetailReturnRepository.Add(purchaseOrderDetailReturn);
        }

        public PurchaseOrderDetailReturn Delete(int id)
        {
            return _purchaseOrderDetailReturnRepository.Delete(id);
        }

        public IEnumerable<PurchaseOrderDetailReturn> GetAll()
        {
            return _purchaseOrderDetailReturnRepository.GetAll();
        }

        public PurchaseOrderDetailReturn GetById(int id)
        {
            return _purchaseOrderDetailReturnRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(PurchaseOrderDetailReturn purchaseOrderDetailReturn)
        {
            _purchaseOrderDetailReturnRepository.Update(purchaseOrderDetailReturn);
        }
    }
}