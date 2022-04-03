using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IProductService : IValidator<Product>
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

        List<Product> GetHomeProducts();
        Product GetProductDetails(string url);
        List<Product> GetCategoryProducts(string url,int page,int pageSize);
        int GetCountByCategory(string category);
        List<Product> GetSearchProduct(string url,int page,int pageSize);
        bool ProductCategoryUpdate(Product entity, int[] categoryIds);
        Product GetByIdWithCategories(int id);
        bool CreateProductAndCategory(Product entity, int[] categoryIds);
        int GetSearchCount(string url);
        bool GetImageName(string fileLocation, string imageName, int imageSize);

        // Cart
        List<Cart> GetCart(string userId); 
        bool AddToCart(string userId,int productId,int quantity);
        void DeleteToCart(string userId, int productId);
        bool CartEdit(string userId, int productId, int quantity);
    }
}