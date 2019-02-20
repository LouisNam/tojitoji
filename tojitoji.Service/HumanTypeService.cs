using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IHumanTypeService
    {
        HumanType Add(HumanType humanType);

        void Update(HumanType humanType);

        HumanType Delete(int id);

        IEnumerable<HumanType> GetAll();

        HumanType GetById(int id);

        void SaveChanges();
    }

    public class HumanTypeService : IHumanTypeService
    {
        private IHumanTypeRepository _humanTypeRepository;
        private IUnitOfWork _unitOfWork;

        public HumanTypeService(IHumanTypeRepository humanTypeRepository, IUnitOfWork unitOfWork)
        {
            this._humanTypeRepository = humanTypeRepository;
            this._unitOfWork = unitOfWork;
        }

        public HumanType Add(HumanType humanType)
        {
            return _humanTypeRepository.Add(humanType);
        }

        public HumanType Delete(int id)
        {
            return _humanTypeRepository.Delete(id);
        }

        public IEnumerable<HumanType> GetAll()
        {
            return _humanTypeRepository.GetAll();
        }

        public HumanType GetById(int id)
        {
            return _humanTypeRepository.GetSingleByCondition(x => x.ID == id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(HumanType humanType)
        {
            _humanTypeRepository.Update(humanType);
        }
    }
}