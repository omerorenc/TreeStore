using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models;

namespace TreeStore.Services
{
    public interface ICategoryService
    {

        IEnumerable<Category> GetCategories();

        List<Category> GetCategories(string User, long id);
        Category GetCategory(long id);

        void CreateCategory(Category Category);
        void UpdateCategory(Category Category);
        void DeleteCategory(long id);
        int CountCategory();
        void SaveCategory();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository CategoryRepository;
        private readonly IUnitOfWork unitOfWork;
 

        #region ICategoryService Members
        public CategoryService(ICategoryRepository CategoryRepository, IUnitOfWork unitOfWork)
        {
            this.CategoryRepository = CategoryRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountCategory()
        {
            return CategoryRepository.GetAll().Count();
        }

        public void CreateCategory(Category Category)
        {
            CategoryRepository.Add(Category);
        }

        public void DeleteCategory(long id)
        {
            CategoryRepository.Delete(c => c.Id == id);
        }

        public List<Category> GetCategories(string User, long id)
        {
            return CategoryRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            var Categories = CategoryRepository.GetAll();
            return Categories;

        }


        public Category GetCategory(long id)
        {
            var Category = CategoryRepository.GetById(id);
            return Category;
        }

        public void SaveCategory()
        {
            unitOfWork.Commit();
        }


        public void UpdateCategory(Category Category)
        {
            CategoryRepository.Update(Category);
        }
        #endregion
    }
}
