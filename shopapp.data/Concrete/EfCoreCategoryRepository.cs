using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        public EfCoreCategoryRepository(ShopContext context): base(context)
        {
            
        }
        private ShopContext ShopContext { get { return context as ShopContext; } }

        public void DeleteProductByCategory(int productId, int categoryId)
        {
            var cmd = "DELETE FROM ProductCategories WHERE ProductId = @p0 and CategoryId = @p1";
            context.Database.ExecuteSqlRaw(cmd,productId,categoryId);
        }

        public Category GetByIdProducts(int id)
        {
            return ShopContext.Categories
                            .Where(i => i.CategoryId == id)
                            .Include(i => i.ProductCategories)
                            .ThenInclude(i => i.Product)
                            .FirstOrDefault();
        }

        public int? GetCategoryCount(int number)
        {
            var categoryControl = ShopContext.Categories.Find(number);

            if (categoryControl == null)
            {
                return null;
            }
            var categoryCount = ShopContext.Products
                                .Where(i => i.IsApproved)
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.ProductCategories.Any(i => i.CategoryId == number));

            return categoryCount.Count();
        }
    }
}