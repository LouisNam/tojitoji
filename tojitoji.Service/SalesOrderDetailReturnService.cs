using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ISalesOrderDetailReturnService
    {
        SalesOrderDetailReturn Add(SalesOrderDetailReturn salesOrderDetailReturn);

        void Update(SalesOrderDetailReturn salesOrderDetailReturn);

        SalesOrderDetailReturn Delete(int id);

        IEnumerable<SalesOrderDetailReturn> GetAll();

        SalesOrderDetailReturn GetById(int id);

        void SaveChanges();
    }

    public class SalesOrderDetailReturnService : ISalesOrderDetailReturnService
    {
        private ISalesOrderDetailReturnRepository _salesOrderDetailReturnRepository;
        private IUnitOfWork _unitOfWork;

        public SalesOrderDetailReturnService(ISalesOrderDetailReturnRepository salesOrderDetailReturnRepository, IUnitOfWork unitOfWork)
        {
            this._salesOrderDetailReturnRepository = salesOrderDetailReturnRepository;
            this._unitOfWork = unitOfWork;
        }

        public SalesOrderDetailReturn Add(SalesOrderDetailReturn salesOrderDetailReturn)
        {
            return _salesOrderDetailReturnRepository.Add(salesOrderDetailReturn);
        }

        public SalesOrderDetailReturn Delete(int id)
        {
            return _salesOrderDetailReturnRepository.Delete(id);
        }

        public IEnumerable<SalesOrderDetailReturn> GetAll()
        {
            return _salesOrderDetailReturnRepository.GetAll();
        }

        public SalesOrderDetailReturn GetById(int id)
        {
            return _salesOrderDetailReturnRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(SalesOrderDetailReturn salesOrderDetailReturn)
        {
            _salesOrderDetailReturnRepository.Update(salesOrderDetailReturn);
        }
    }
}