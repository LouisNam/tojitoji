using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll();

        Transaction GetById(int id);

        Transaction Add(Transaction Transaction);

        void Update(Transaction Transaction);

        Transaction Delete(int id);

        void SaveChanges();
    }

    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;
        private IUnitOfWork _unitOfWork;

        public TransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            this._transactionRepository = transactionRepository;
            this._unitOfWork = unitOfWork;
        }

        public Transaction Add(Transaction transaction)
        {
            return _transactionRepository.Add(transaction);
        }

        public Transaction Delete(int id)
        {
            return _transactionRepository.Delete(id);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _transactionRepository.GetAll();
        }

        public Transaction GetById(int id)
        {
            return _transactionRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Transaction transaction)
        {
            _transactionRepository.Update(transaction);
        }
    }
}