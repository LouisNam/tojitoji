using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ICoSoKinhDoanhService
    {
        CoSoKinhDoanh Add(CoSoKinhDoanh coSoKinhDoanh);

        void Update(CoSoKinhDoanh coSoKinhDoanh);

        CoSoKinhDoanh Delete(int id);

        IEnumerable<CoSoKinhDoanh> GetAll();

        IEnumerable<CoSoKinhDoanh> GetListCoSoKinhDoanh();

        CoSoKinhDoanh GetById(int id);

        void SaveChanges();
    }

    public class CoSoKinhDoanhService : ICoSoKinhDoanhService
    {
        private ICoSoKinhDoanhRepository _coSoKinhDoanhRepository;
        private IUnitOfWork _unitOfWork;

        public CoSoKinhDoanhService(ICoSoKinhDoanhRepository coSoKinhDoanhRepository, IUnitOfWork unitOfWork)
        {
            this._coSoKinhDoanhRepository = coSoKinhDoanhRepository;
            this._unitOfWork = unitOfWork;
        }

        public CoSoKinhDoanh Add(CoSoKinhDoanh coSoKinhDoanh)
        {
            return _coSoKinhDoanhRepository.Add(coSoKinhDoanh);
        }

        public CoSoKinhDoanh Delete(int id)
        {
            return _coSoKinhDoanhRepository.Delete(id);
        }

        public IEnumerable<CoSoKinhDoanh> GetAll()
        {
            return _coSoKinhDoanhRepository.GetAll();
        }

        public CoSoKinhDoanh GetById(int id)
        {
            return _coSoKinhDoanhRepository.GetSingleById(id);
        }

        public IEnumerable<CoSoKinhDoanh> GetListCoSoKinhDoanh()
        {
            IEnumerable<CoSoKinhDoanh> query = _coSoKinhDoanhRepository.GetAll();
            return query;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(CoSoKinhDoanh coSoKinhDoanh)
        {
            _coSoKinhDoanhRepository.Update(coSoKinhDoanh);
        }
    }
}