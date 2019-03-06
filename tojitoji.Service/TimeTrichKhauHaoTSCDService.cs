using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ITimeTrichKhauHaoTSCDService
    {
        TimeTrichKhauHaoTSCD Add(TimeTrichKhauHaoTSCD timeTrichKhauHaoTSCD);

        void Update(TimeTrichKhauHaoTSCD timeTrichKhauHaoTSCD);

        TimeTrichKhauHaoTSCD Delete(int id);

        IEnumerable<TimeTrichKhauHaoTSCD> GetAll();

        IEnumerable<TimeTrichKhauHaoTSCD> GetAll(string keyword);

        TimeTrichKhauHaoTSCD GetById(int id);

        void SaveChanges();
    }

    public class TimeTrichKhauHaoTSCDService : ITimeTrichKhauHaoTSCDService
    {
        private ITimeTrichKhauHaoTSCDRepository _timeTrichKhauHaoTSCDRepository;
        private IUnitOfWork _unitOfWork;

        public TimeTrichKhauHaoTSCDService(ITimeTrichKhauHaoTSCDRepository timeTrichKhauHaoTSCDRepository, IUnitOfWork unitOfWork)
        {
            this._timeTrichKhauHaoTSCDRepository = timeTrichKhauHaoTSCDRepository;
            this._unitOfWork = unitOfWork;
        }

        public TimeTrichKhauHaoTSCD Add(TimeTrichKhauHaoTSCD timeTrichKhauHaoTSCD)
        {
            return _timeTrichKhauHaoTSCDRepository.Add(timeTrichKhauHaoTSCD);
        }

        public TimeTrichKhauHaoTSCD Delete(int id)
        {
            return _timeTrichKhauHaoTSCDRepository.Delete(id);
        }

        public IEnumerable<TimeTrichKhauHaoTSCD> GetAll()
        {
            return _timeTrichKhauHaoTSCDRepository.GetAll();
        }

        public IEnumerable<TimeTrichKhauHaoTSCD> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _timeTrichKhauHaoTSCDRepository.GetMulti(x => x.DanhMucNhomTSCD.Contains(keyword));
            else
                return _timeTrichKhauHaoTSCDRepository.GetAll();
        }

        public TimeTrichKhauHaoTSCD GetById(int id)
        {
            return _timeTrichKhauHaoTSCDRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(TimeTrichKhauHaoTSCD timeTrichKhauHaoTSCD)
        {
            _timeTrichKhauHaoTSCDRepository.Update(timeTrichKhauHaoTSCD);
        }
    }
}