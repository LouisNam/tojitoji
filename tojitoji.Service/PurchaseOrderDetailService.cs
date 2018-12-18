using System;
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

        IEnumerable<PurchaseOrderDetail> GetAll(int id);

        PurchaseOrderDetail GetById(int id);

        void SaveChanges();

        void CreatePurchaseOrderDetail(int productID, int purchaseOrderID, decimal price, int quantity);
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

        public void CreatePurchaseOrderDetail(int productID, int purchaseOrderID, decimal price, int quantity)
        {
            _purchaseOrderDetailRepository.CreatePurchaseOrderDetail(productID, purchaseOrderID, price, quantity);
        }

        public PurchaseOrderDetail Delete(int id)
        {
            return _purchaseOrderDetailRepository.Delete(id);
        }

        public IEnumerable<PurchaseOrderDetail> GetAll(int id)
        {
            return _purchaseOrderDetailRepository.GetMulti(x => x.PurchaseOrderID == id);
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