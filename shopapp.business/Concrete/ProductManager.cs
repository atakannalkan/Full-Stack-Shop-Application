using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private data.Concrete.ShopContext _context;
        public ProductManager(IUnitOfWork unitOfWork, data.Concrete.ShopContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // İş kurallarını uygula !
        public void Create(Product entity)
        {
            _unitOfWork.Products.Create(entity);
            _unitOfWork.Save();
        }

        public bool CreateProductAndCategory(Product entity, int[] categoryIds)
        {
            if (Validation(entity))
            {            
                if (categoryIds.Length == 0)
                {
                    ErrorMessage += "You must select at least 1 category !\n";
                    return false;
                }
                _unitOfWork.Products.CreateProductAndCategory(entity, categoryIds);
                _unitOfWork.Save();
                return true;
            }
            return false;
            
        }

        public void Delete(Product entity)
        {
            _unitOfWork.Products.Delete(entity);
            _unitOfWork.Save();
        }
        public List<Product> GetAll()
        {
            return _unitOfWork.Products.GetAll();
        }
        public Product GetById(int id)
        {
            return _unitOfWork.Products.GetById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _unitOfWork.Products.GetByIdWithCategories(id);
        }

        public List<Product> GetCategoryProducts(string url,int page,int pageSize)
        {
            return _unitOfWork.Products.GetCategoryProducts(url,page,pageSize);
        }

        public int GetCountByCategory(string category)
        {
            return _unitOfWork.Products.GetCountByCategory(category);
        }

        public List<Product> GetHomeProducts()
        {
            return _unitOfWork.Products.GetHomeProducts();
        }

        public Product GetProductDetails(string url)
        {
            return _unitOfWork.Products.GetProductDetails(url);
        }

        public List<Product> GetSearchProduct(string url,int page,int pageSize)
        {
            return _unitOfWork.Products.GetSearchProduct(url,page,pageSize);
        }

        public bool ProductCategoryUpdate(Product entity, int[] categoryIds)
        {
            if (Validation(entity))
            {
                if (categoryIds.Length == 0)
                {
                    ErrorMessage += "You must select at least 1 category !\n";
                    return false;
                }
                _unitOfWork.Products.ProductCategoryUpdate(entity, categoryIds);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public void Update(Product entity)
        {
            _unitOfWork.Products.Update(entity);
            _unitOfWork.Save();
        }       

        public int GetSearchCount(string url)
        {
            return _unitOfWork.Products.GetSearchCount(url);
        }

        public bool GetImageName(string fileLocation,string imageName, int imageSize)
        {
            if (imageSize > 512000)
            {
                ErrorMessage += "Image size cannot exceed 500 kilobytes !\n";
                return false;
            }
            var result = _unitOfWork.Products.GetImageName(fileLocation, imageName, imageSize);

            if (result == false)
            {
                ErrorMessage += "The entered image name already exists !\n";
                return false;
            }
            return true;
        }

        public List<Cart> GetCart(string userId)
        {
            return _unitOfWork.Products.GetCart(userId);
        }

        public bool AddToCart(string userId, int productId, int quantity)
        {
            var product = _unitOfWork.Products.GetById(productId);
            var cart = new Cart();

            cart = _context.Carts.Where(i => i.UserId == userId && i.ProductId == productId).FirstOrDefault();

            if (cart != null && product.Stock < (cart.Quantity+quantity))
            {
                ErrorMessage += $"Bu üründen {cart.Quantity+quantity} adet alamazsınız. Stokta sadece {product.Stock} adet ürün var !";
                return false;
            }

            _unitOfWork.Products.AddToCart(userId, productId, quantity);
            _unitOfWork.Save();
            return true;
        }

        public void DeleteToCart(string userId, int productId)
        {
            _unitOfWork.Products.DeleteToCart(userId, productId);
            _unitOfWork.Save();
        }

        public bool CartEdit(string userId, int productId, int quantity)
        {
            var product = _unitOfWork.Products.GetById(productId);
            if(quantity <= 0)
            {
                ErrorMessage += "Adet sayısı 0'a eşit olamaz !";
                return false;
            }
            if(product.Stock < quantity)
            {
                ErrorMessage += $"Bu üründen {quantity} adet alamazsınız. Stokta sadece {product.Stock} ürün var !";
                return false;
            }

            _unitOfWork.Products.CartEdit(userId, productId, quantity);
            _unitOfWork.Save();
            return true;
        }

        // Validation
        public string ErrorMessage { get; set; }
        public bool Validation(Product entity)
        {
            var isValid = true;


            var product = _context.Products.FirstOrDefault(i => i.Url == entity.Url && i.ProductId != entity.ProductId);
            if (product != null)
            {
                // Burada veritabanından 'url' alanını benzersiz yaptığım için kontrolünü sağlıyorum
                ErrorMessage += "The entered url already exists !\n";
                isValid = false;
            }

            return isValid;
        }
    }
}