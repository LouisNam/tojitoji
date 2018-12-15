using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IPurchaseOrderDetailService
    {
        PurchaseOrderDetail Add(PurchaseOrderDetail purchaseOrderDetail);

        void Update(PurchaseOrderDetail purchaseOrderDetail);

        PurchaseOrderDetail Delete(int id);

        IEnumerable<PurchaseOrderDetail> GetAll();

        PurchaseOrderDetail GetById(int id);

        void SaveChanges();
    }

    public class PurchaseOrderDetailService : IPurchaseOrderDetailService
    {
        private IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public PurchaseOrderDetailService(IPurchaseOrderDetailRepository purchaseOrderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._purchaseOrderDetailRepository = purchaseOrderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public PurchaseOrderDetail Add(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _purchaseOrderDetailRepository.Add(purchaseOrderDetail);
        }

        public PurchaseOrderDetail Delete(int id)
        {
            return _purchaseOrderDetailRepository.Delete(id);
        }

        public IEnumerable<PurchaseOrderDetail> GetAll()
        {
            return _purchaseOrderDetailRepository.GetAll();
        }

        public PurchaseOrderDetail GetById(int id)
        {
            return _purchaseOrderDetailRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(PurchaseOrderDetail purchaseOrderDetail)
        {
            _purchaseOrderDetailRepository.Update(purchaseOrderDetail);
        }
    }
}