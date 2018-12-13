using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IInventoryTransactionService
    {
        InventoryTransaction Add(InventoryTransaction inventoryTransaction);

        void Update(InventoryTransaction inventoryTransaction);

        InventoryTransaction Delete(int id);

        IEnumerable<InventoryTransaction> GetAll();

        InventoryTransaction GetById(int id);

        void SaveChanges();
    }

    public class InventoryTransactionService : IInventoryTransactionService
    {
        private IInventoryTransactionRepository _inventoryTransactionRepository;
        private IUnitOfWork _unitOfWork;

        public InventoryTransactionService(IInventoryTransactionRepository inventoryTransactionRepository, IUnitOfWork unitOfWork)
        {
            this._inventoryTransactionRepository = inventoryTransactionRepository;
            this._unitOfWork = unitOfWork;
        }

        public InventoryTransaction Add(InventoryTransaction inventoryTransaction)
        {
            return _inventoryTransactionRepository.Add(inventoryTransaction);
        }

        public InventoryTransaction Delete(int id)
        {
            return _inventoryTransactionRepository.Delete(id);
        }

        public IEnumerable<InventoryTransaction> GetAll()
        {
            return _inventoryTransactionRepository.GetAll();
        }

        public InventoryTransaction GetById(int id)
        {
            return _inventoryTransactionRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(InventoryTransaction inventoryTransaction)
        {
            _inventoryTransactionRepository.Update(inventoryTransaction);
        }
    }
}