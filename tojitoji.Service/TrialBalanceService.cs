using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ITrialBalanceService
    {
        TrialBalance Add(TrialBalance trialBalance);

        void Update(TrialBalance trialBalance);

        TrialBalance Delete(int id);

        IEnumerable<TrialBalance> GetAll();

        TrialBalance GetById(int id);

        void SaveChanges();
    }

    public class TrialBalanceService : ITrialBalanceService
    {
        private ITrialBalanceRepository _trialBalanceRepository;
        private IUnitOfWork _unitOfWork;

        public TrialBalanceService(ITrialBalanceRepository trialBalanceRepository, IUnitOfWork unitOfWork)
        {
            this._trialBalanceRepository = trialBalanceRepository;
            this._unitOfWork = unitOfWork;
        }

        public TrialBalance Add(TrialBalance TrialBalance)
        {
            return _trialBalanceRepository.Add(TrialBalance);
        }

        public TrialBalance Delete(int id)
        {
            return _trialBalanceRepository.Delete(id);
        }

        public IEnumerable<TrialBalance> GetAll()
        {
            return _trialBalanceRepository.GetAll();
        }

        public TrialBalance GetById(int id)
        {
            return _trialBalanceRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(TrialBalance TrialBalance)
        {
            _trialBalanceRepository.Update(TrialBalance);
        }
    }
}