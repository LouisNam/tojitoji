using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ISalesOrderService
    {
        SalesOrder Add(SalesOrder salesOrder);

        void Update(SalesOrder salesOrder);

        SalesOrder Delete(int id);

        IEnumerable<SalesOrder> GetAll();

        SalesOrder GetById(int id);

        void SaveChanges();
    }

    public class SalesOrderService : ISalesOrderService
    {
        private ISalesOrderRepository _salesOrderRepository;
        private IUnitOfWork _unitOfWork;

        public SalesOrderService(ISalesOrderRepository salesOrderRepository, IUnitOfWork unitOfWork)
        {
            this._salesOrderRepository = salesOrderRepository;
            this._unitOfWork = unitOfWork;
        }

        public SalesOrder Add(SalesOrder salesOrder)
        {
            return _salesOrderRepository.Add(salesOrder);
        }

        public SalesOrder Delete(int id)
        {
            return _salesOrderRepository.Delete(id);
        }

        public IEnumerable<SalesOrder> GetAll()
        {
            return _salesOrderRepository.GetAll();
        }

        public SalesOrder GetById(int id)
        {
            return _salesOrderRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(SalesOrder salesOrder)
        {
            _salesOrderRepository.Update(salesOrder);
        }
    }
}