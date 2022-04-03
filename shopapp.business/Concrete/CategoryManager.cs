using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private data.Concrete.ShopContext _context;
        public CategoryManager(IUnitOfWork unitOfWork, data.Concrete.ShopContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // İş kurallarını uygula !
        public bool Create(Category entity)
        {
            if (Validation(entity))
            {
                _unitOfWork.Categories.Create(entity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
        public void Delete(Category entity)
        {
            _unitOfWork.Categories.Delete(entity);
            _unitOfWork.Save();
        }
        public List<Category> GetAll()
        {
            return _unitOfWork.Categories.GetAll();
        }
        public Category GetById(int id)
        {
            return _unitOfWork.Categories.GetById(id);
        }
        public void Update(Category entity)
        {
            _unitOfWork.Categories.Update(entity);
            _unitOfWork.Save();
        }        

        public int? GetCategoryCount(int number)
        {
            return _unitOfWork.Categories.GetCategoryCount(number);
        }

        public Category GetByIdProducts(int id)
        {
            return _unitOfWork.Categories.GetByIdProducts(id);
        }

        public void DeleteProductByCategory(int productId, int categoryId)
        {
            _unitOfWork.Categories.DeleteProductByCategory(productId, categoryId);
            _unitOfWork.Save();
        }

        // Validation
        public string ErrorMessage { get; set; }
        public bool Validation(Category entity)
        {
            var isValid = true;

            var categorySingle = _context.Categories.Where(i => i.Name == entity.Name || i.Url == entity.Url).FirstOrDefault();
            if (categorySingle != null)
            {
                ErrorMessage += "Category name or url already in use !\n";
                isValid = false;
            }
            return isValid;
        }
    }
}