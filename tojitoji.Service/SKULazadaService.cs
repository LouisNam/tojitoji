using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ISKULazadaService
    {
        IEnumerable<SKULazada> GetAll();

        IEnumerable<SKULazada> GetAll(string keyword);

        SKULazada GetById(int id);

        SKULazada Add(SKULazada sKULazada);

        void Update(SKULazada sKULazada);

        SKULazada Delete(int id);

        void SaveChanges();
    }

    public class SKULazadaService : ISKULazadaService
    {
        private ISKULazadaRepository _sKULazadaRepository;
        private IUnitOfWork _unitOfWork;

        public SKULazadaService(ISKULazadaRepository sKULazadaRepository, IUnitOfWork unitOfWork)
        {
            this._sKULazadaRepository = sKULazadaRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SKULazada> GetAll()
        {
            return _sKULazadaRepository.GetAll();
        }

        public IEnumerable<SKULazada> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _sKULazadaRepository.GetMulti(x => x.SKUName.Contains(keyword));
            else
                return _sKULazadaRepository.GetAll();
        }

        public SKULazada GetById(int id)
        {
            return _sKULazadaRepository.GetSingleById(id);
        }

        public SKULazada Add(SKULazada sKULazada)
        {
            return _sKULazadaRepository.Add(sKULazada);
        }

        public void Update(SKULazada sKULazada)
        {
            _sKULazadaRepository.Update(sKULazada);
        }

        public SKULazada Delete(int id)
        {
            return _sKULazadaRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}