using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ILoaiTaiSanService
    {
        LoaiTaiSan Add(LoaiTaiSan loaiTaiSan);

        void Update(LoaiTaiSan loaiTaiSan);

        LoaiTaiSan Delete(int id);

        IEnumerable<LoaiTaiSan> GetAll();

        IEnumerable<LoaiTaiSan> GetAll(string keyword);

        LoaiTaiSan GetById(int id);

        void SaveChanges();
    }

    public class LoaiTaiSanService : ILoaiTaiSanService
    {
        private ILoaiTaiSanRepository _loaiTaiSanRepository;
        private IUnitOfWork _unitOfWork;

        public LoaiTaiSanService(ILoaiTaiSanRepository loaiTaiSanRepository, IUnitOfWork unitOfWork)
        {
            this._loaiTaiSanRepository = loaiTaiSanRepository;
            this._unitOfWork = unitOfWork;
        }

        public LoaiTaiSan Add(LoaiTaiSan loaiTaiSan)
        {
            return _loaiTaiSanRepository.Add(loaiTaiSan);
        }

        public LoaiTaiSan Delete(int id)
        {
            return _loaiTaiSanRepository.Delete(id);
        }

        public IEnumerable<LoaiTaiSan> GetAll()
        {
            return _loaiTaiSanRepository.GetAll();
        }

        public IEnumerable<LoaiTaiSan> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _loaiTaiSanRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _loaiTaiSanRepository.GetAll();
        }

        public LoaiTaiSan GetById(int id)
        {
            return _loaiTaiSanRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(LoaiTaiSan loaiTaiSan)
        {
            _loaiTaiSanRepository.Update(loaiTaiSan);
        }
    }
}