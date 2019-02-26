using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ITaiSanService
    {
        TaiSan Add(TaiSan taiSan);

        void Update(TaiSan taiSan);

        TaiSan Delete(int id);

        IEnumerable<TaiSan> GetAll();

        IEnumerable<TaiSan> GetAll(string keyword);

        TaiSan GetById(int id);

        void SaveChanges();
    }

    public class TaiSanService : ITaiSanService
    {
        private ITaiSanRepository _taiSanRepository;
        private IUnitOfWork _unitOfWork;

        public TaiSanService(ITaiSanRepository taiSanRepository, IUnitOfWork unitOfWork)
        {
            this._taiSanRepository = taiSanRepository;
            this._unitOfWork = unitOfWork;
        }

        public TaiSan Add(TaiSan taiSan)
        {
            return _taiSanRepository.Add(taiSan);
        }

        public TaiSan Delete(int id)
        {
            return _taiSanRepository.Delete(id);
        }

        public IEnumerable<TaiSan> GetAll()
        {
            return _taiSanRepository.GetAll(new string[] { "LoaiTaiSan" });
        }

        public IEnumerable<TaiSan> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _taiSanRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _taiSanRepository.GetAll(new string[] { "LoaiTaiSan" });
        }

        public TaiSan GetById(int id)
        {
            return _taiSanRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(TaiSan taiSan)
        {
            _taiSanRepository.Update(taiSan);
        }
    }
}