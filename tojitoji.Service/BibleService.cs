using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface IBibleService
    {
        Bible Add(Bible bible);

        void Update(Bible bible);

        Bible Delete(int id);

        IEnumerable<Bible> GetAll();

        IEnumerable<Bible> GetAll(string keyword);

        IEnumerable<Bible> GetListBible(string keyword);

        Bible GetById(int id);

        void SaveChanges();
    }

    public class BibleService : IBibleService
    {
        private IBibleRepository _bibleRepository;
        private IUnitOfWork _unitOfWork;

        public BibleService(IBibleRepository bibleRepository, IUnitOfWork unitOfWork)
        {
            this._bibleRepository = bibleRepository;
            this._unitOfWork = unitOfWork;
        }

        public Bible Add(Bible bible)
        {
            return _bibleRepository.Add(bible);
        }

        public Bible Delete(int id)
        {
            return _bibleRepository.Delete(id);
        }

        public IEnumerable<Bible> GetAll()
        {
            return _bibleRepository.GetAll();
        }

        public IEnumerable<Bible> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _bibleRepository.GetMulti(x => x.Shortcut.Contains(keyword));
            else
                return _bibleRepository.GetAll();
        }

        public Bible GetById(int id)
        {
            return _bibleRepository.GetSingleById(id);
        }

        public IEnumerable<Bible> GetListBible(string keyword)
        {
            IEnumerable<Bible> query;
            if (!string.IsNullOrEmpty(keyword))
                query = _bibleRepository.GetMulti(x => x.Shortcut.Contains(keyword));
            else
                query = _bibleRepository.GetAll();
            return query;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Bible bible)
        {
            _bibleRepository.Update(bible);
        }
    }
}