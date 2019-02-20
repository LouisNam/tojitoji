using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();

        IEnumerable<Category> GetAll(string keyword);

        IEnumerable<Category> GetListCategory();

        Category GetById(int id);

        Category Add(Category category);

        void Update(Category category);

        Category Delete(int id);

        void SaveChanges();
    }

    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this._categoryRepository = categoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public Category Add(Category category)
        {
            return _categoryRepository.Add(category);
        }

        public Category Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public IEnumerable<Category> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _categoryRepository.GetMulti(x => x.Name_1.Contains(keyword));
            else
                return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetSingleById(id);
        }

        public IEnumerable<Category> GetListCategory()
        {
            IEnumerable<Category> query;
            query = _categoryRepository.GetAll();
            return query;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}