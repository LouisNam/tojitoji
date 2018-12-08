using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IHumanService
    {
        Human Add(Human human);

        void Update(Human humanTyp);

        Human Delete(int id);

        IEnumerable<Human> GetAll();

        IEnumerable<Human> GetAll(string keyword);

        Human GetById(int id);

        void SaveChanges();
    }

    public class HumanService : IHumanService
    {
        private IHumanRepository _humanRepository;
        private IUnitOfWork _unitOfWork;

        public HumanService(IHumanRepository humanRepository, IUnitOfWork unitOfWork)
        {
            this._humanRepository = humanRepository;
            this._unitOfWork = unitOfWork;
        }

        public Human Add(Human human)
        {
            return _humanRepository.Add(human);
        }

        public Human Delete(int id)
        {
            return _humanRepository.Delete(id);
        }

        public IEnumerable<Human> GetAll()
        {
            return _humanRepository.GetAll();
        }

        public IEnumerable<Human> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _humanRepository.GetMulti(x => x.FirstName.Contains(keyword));
            else
                return _humanRepository.GetAll();
        }

        public Human GetById(int id)
        {
            return _humanRepository.GetSingleByCondition(x => x.ID == id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Human human)
        {
            _humanRepository.Update(human);
        }
    }
}