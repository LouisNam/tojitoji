using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IKhoService
    {
        Kho Add(Kho kho);

        void Update(Kho kho);

        Kho Delete(int id);

        IEnumerable<Kho> GetAll();

        IEnumerable<Kho> GetAll(string keyword);

        Kho GetById(int id);

        void SaveChanges();
    }

    public class KhoService : IKhoService
    {
        private IKhoRepository _khoRepository;
        private IUnitOfWork _unitOfWork;

        public KhoService(IKhoRepository khoRepository, IUnitOfWork unitOfWork)
        {
            this._khoRepository = khoRepository;
            this._unitOfWork = unitOfWork;
        }

        public Kho Add(Kho kho)
        {
            return _khoRepository.Add(kho);
        }

        public Kho Delete(int id)
        {
            return _khoRepository.Delete(id);
        }

        public IEnumerable<Kho> GetAll()
        {
            return _khoRepository.GetAll();
        }

        public IEnumerable<Kho> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _khoRepository.GetMulti(x => x.Kho_3.Contains(keyword) || x.Kho_4.Contains(keyword));
            else
                return _khoRepository.GetAll();
        }

        public Kho GetById(int id)
        {
            return _khoRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Kho kho)
        {
            _khoRepository.Update(kho);
        }
    }
}