using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ITangGiamTSCDService
    {
        TangGiamTSCD Add(TangGiamTSCD tangGiamTSCD);

        void Update(TangGiamTSCD tangGiamTSCD);

        TangGiamTSCD Delete(int id);

        IEnumerable<TangGiamTSCD> GetAll();

        IEnumerable<TangGiamTSCD> GetAll(string keyword);

        TangGiamTSCD GetById(int id);

        void SaveChanges();
    }

    public class TangGiamTSCDService : ITangGiamTSCDService
    {
        private ITangGiamTSCDRepository _tangGiamTSCDRepository;
        private IUnitOfWork _unitOfWork;

        public TangGiamTSCDService(ITangGiamTSCDRepository tangGiamTSCDRepository, IUnitOfWork unitOfWork)
        {
            this._tangGiamTSCDRepository = tangGiamTSCDRepository;
            this._unitOfWork = unitOfWork;
        }

        public TangGiamTSCD Add(TangGiamTSCD tangGiamTSCD)
        {
            return _tangGiamTSCDRepository.Add(tangGiamTSCD);
        }

        public TangGiamTSCD Delete(int id)
        {
            return _tangGiamTSCDRepository.Delete(id);
        }

        public IEnumerable<TangGiamTSCD> GetAll()
        {
            return _tangGiamTSCDRepository.GetAll();
        }

        public IEnumerable<TangGiamTSCD> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _tangGiamTSCDRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _tangGiamTSCDRepository.GetAll();
        }

        public TangGiamTSCD GetById(int id)
        {
            return _tangGiamTSCDRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(TangGiamTSCD tangGiamTSCD)
        {
            _tangGiamTSCDRepository.Update(tangGiamTSCD);
        }
    }
}