using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IBundleService
    {
        IEnumerable<Bundle> GetAll();

        IEnumerable<Bundle> GetAll(string keyword);

        Bundle GetById(int id);

        Bundle Add(Bundle bundle);

        void Update(Bundle bundle);

        Bundle Delete(int id);

        void SaveChanges();
    }

    public class BundleService : IBundleService
    {
        private IBundleRepository _bundleRepository;
        private IUnitOfWork _unitOfWork;

        public BundleService(IBundleRepository bundleRepository, IUnitOfWork unitOfWork)
        {
            this._bundleRepository = bundleRepository;
            this._unitOfWork = unitOfWork;
        }

        public Bundle Add(Bundle bundle)
        {
            return _bundleRepository.Add(bundle);
        }

        public Bundle Delete(int id)
        {
            return _bundleRepository.Delete(id);
        }

        public IEnumerable<Bundle> GetAll()
        {
            return _bundleRepository.GetAll();
        }

        public IEnumerable<Bundle> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _bundleRepository.GetMulti(x => x.BundleName.Contains(keyword) || x.BundleType.Contains(keyword));
            else
                return _bundleRepository.GetAll();
        }

        public Bundle GetById(int id)
        {
            return _bundleRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Bundle bundle)
        {
            _bundleRepository.Update(bundle);
        }
    }
}