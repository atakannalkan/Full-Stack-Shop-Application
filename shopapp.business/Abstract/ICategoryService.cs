using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface ICategoryService : IValidator<Category>
    {
        List<Category> GetAll();
        Category GetById(int id);
        bool Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);

        int? GetCategoryCount(int number);
        Category GetByIdProducts(int id);
        void DeleteProductByCategory(int productId, int categoryId);
    }
}