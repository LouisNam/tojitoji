using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IWarehouseService
    {
        Warehouse Add(Warehouse warehouse);

        void Update(Warehouse warehouse);

        Warehouse Delete(int id);

        IEnumerable<Warehouse> GetAll();

        IEnumerable<Warehouse> GetAll(string keyword);

        Warehouse GetById(int id);

        void SaveChanges();
    }

    public class WarehouseService : IWarehouseService
    {
        private IWarehouseRepository _warehouseRepository;
        private IUnitOfWork _unitOfWork;

        public WarehouseService(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
        {
            this._warehouseRepository = warehouseRepository;
            this._unitOfWork = unitOfWork;
        }

        public Warehouse Add(Warehouse warehouse)
        {
            return _warehouseRepository.Add(warehouse);
        }

        public Warehouse Delete(int id)
        {
            return _warehouseRepository.Delete(id);
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return _warehouseRepository.GetAll();
        }

        public IEnumerable<Warehouse> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _warehouseRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _warehouseRepository.GetAll();
        }

        public Warehouse GetById(int id)
        {
            return _warehouseRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Warehouse warehouse)
        {
            _warehouseRepository.Update(warehouse);
        }
    }
}