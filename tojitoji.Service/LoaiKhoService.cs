using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ILoaiKhoService
    {
        LoaiKho Add(LoaiKho loaiKho);

        void Update(LoaiKho loaiKho);

        LoaiKho Delete(int id);

        IEnumerable<LoaiKho> GetAll();

        IEnumerable<LoaiKho> GetAll(string keyword);

        LoaiKho GetById(int id);

        void SaveChanges();
    }

    public class LoaiKhoService : ILoaiKhoService
    {
        private ILoaiKhoRepository _loaiKhoRepository;
        private IUnitOfWork _unitOfWork;

        public LoaiKhoService(ILoaiKhoRepository loaiKhoRepository, IUnitOfWork unitOfWork)
        {
            this._loaiKhoRepository = loaiKhoRepository;
            this._unitOfWork = unitOfWork;
        }

        public LoaiKho Add(LoaiKho loaiKho)
        {
            return _loaiKhoRepository.Add(loaiKho);
        }

        public LoaiKho Delete(int id)
        {
            return _loaiKhoRepository.Delete(id);
        }

        public IEnumerable<LoaiKho> GetAll()
        {
            return _loaiKhoRepository.GetAll();
        }

        public IEnumerable<LoaiKho> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _loaiKhoRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _loaiKhoRepository.GetAll();
        }

        public LoaiKho GetById(int id)
        {
            return _loaiKhoRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(LoaiKho loaiKho)
        {
            _loaiKhoRepository.Update(loaiKho);
        }
    }
}