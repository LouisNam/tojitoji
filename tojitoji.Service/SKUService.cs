using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ISKUService
    {
        IEnumerable<SKU> GetAll();

        SKU GetById(int id);

        SKU Add(SKU sKU);

        void Update(SKU sKU);

        SKU Delete(int id);

        void SaveChanges();
    }

    public class SKUService : ISKUService
    {
        private ISKURepository _sKURepository;
        private IUnitOfWork _unitOfWork;

        public SKUService(ISKURepository sKURepository, IUnitOfWork unitOfWork)
        {
            this._sKURepository = sKURepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<SKU> GetAll()
        {
            return _sKURepository.GetAll(new string[] { "Product", "Bundle" });
        }

        public SKU GetById(int id)
        {
            return _sKURepository.GetSingleById(id);
        }

        public SKU Add(SKU sKU)
        {
            return _sKURepository.Add(sKU);
        }

        public void Update(SKU sKU)
        {
            _sKURepository.Update(sKU);
        }

        public SKU Delete(int id)
        {
            return _sKURepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}