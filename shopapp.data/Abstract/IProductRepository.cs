using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetHomeProducts();
        Product GetProductDetails(string url);
        List<Product> GetCategoryProducts(string url,int page,int pageSize);
        int GetCountByCategory(string category);
        List<Product> GetSearchProduct(string url,int page,int pageSize);
        void ProductCategoryUpdate(Product entity, int[] categoryIds);
        Product GetByIdWithCategories(int id);
        void CreateProductAndCategory(Product entity, int[] categoryIds);
        int GetSearchCount(string url);
        bool GetImageName(string fileLocation, string imageName, int imageSize);
        List<Cart> GetCart(string userId);
        void AddToCart(string userId,int productId,int quantity);
        void DeleteToCart(string userId, int productId);
        void CartEdit(string userId, int productId, int quantity);
    }
}