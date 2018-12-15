using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IPurchaseOrderService
    {
        PurchaseOrder Add(PurchaseOrder purchaseOrder);

        void Update(PurchaseOrder purchaseOrder);

        PurchaseOrder Delete(int id);

        IEnumerable<PurchaseOrder> GetAll();

        PurchaseOrder GetById(int id);

        void SaveChanges();
    }

    public class PurchaseOrderService : IPurchaseOrderService
    {
        private IPurchaseOrderRepository _purchaseOrderRepository;
        private IUnitOfWork _unitOfWork;

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository, IUnitOfWork unitOfWork)
        {
            this._purchaseOrderRepository = purchaseOrderRepository;
            this._unitOfWork = unitOfWork;
        }

        public PurchaseOrder Add(PurchaseOrder purchaseOrder)
        {
            return _purchaseOrderRepository.Add(purchaseOrder);
        }

        public PurchaseOrder Delete(int id)
        {
            return _purchaseOrderRepository.Delete(id);
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            return _purchaseOrderRepository.GetAll();
        }

        public PurchaseOrder GetById(int id)
        {
            return _purchaseOrderRepository.GetSingleByCondition(x => x.ID == id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderRepository.Update(purchaseOrder);
        }
    }
}