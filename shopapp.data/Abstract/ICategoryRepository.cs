using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        int? GetCategoryCount(int number);

        Category GetByIdProducts(int id);
        void DeleteProductByCategory(int productId, int categoryId);
    }
}