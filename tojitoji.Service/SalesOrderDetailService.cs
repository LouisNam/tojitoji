using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ISalesOrderDetailService
    {
        SalesOrderDetail Add(SalesOrderDetail salesOrderDetail);

        void Update(SalesOrderDetail salesOrderDetail);

        SalesOrderDetail Delete(int id);

        IEnumerable<SalesOrderDetail> GetAll(int id);

        SalesOrderDetail GetById(int id);

        void SaveChanges();
    }

    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private ISalesOrderDetailRepository _salesOrderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public SalesOrderDetailService(ISalesOrderDetailRepository salesOrderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._salesOrderDetailRepository = salesOrderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public SalesOrderDetail Add(SalesOrderDetail salesOrderDetail)
        {
            return _salesOrderDetailRepository.Add(salesOrderDetail);
        }

        public SalesOrderDetail Delete(int id)
        {
            return _salesOrderDetailRepository.Delete(id);
        }

        public IEnumerable<SalesOrderDetail> GetAll(int id)
        {
            return _salesOrderDetailRepository.GetMulti(x => x.SalesOrderID == id);
        }

        public SalesOrderDetail GetById(int id)
        {
            return _salesOrderDetailRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(SalesOrderDetail salesOrderDetail)
        {
            _salesOrderDetailRepository.Update(salesOrderDetail);
        }
    }
}