using System;
using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IAccountService
    {
        Account Add(Account Account);

        void Update(Account Account);

        Account Delete(int id);

        IEnumerable<Account> GetAll();

        IEnumerable<Account> GetAll(string keyword);

        Account GetById(int id);

        void SaveChanges();
    }

    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        private IUnitOfWork _unitOfWork;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            this._accountRepository = accountRepository;
            this._unitOfWork = unitOfWork;
        }

        public Account Add(Account Account)
        {
            return _accountRepository.Add(Account);
        }

        public Account Delete(int id)
        {
            return _accountRepository.Delete(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public IEnumerable<Account> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _accountRepository.GetMulti(x => x.Account_Name.Contains(keyword));
            else
                return _accountRepository.GetAll();
        }

        public Account GetById(int id)
        {
            return _accountRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Account account)
        {
            _accountRepository.Update(account);
        }
    }
}