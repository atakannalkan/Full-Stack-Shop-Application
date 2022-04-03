using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product>, IProductRepository
    {
        public EfCoreProductRepository(ShopContext context): base(context)
        {
            
        }
        private ShopContext ShopContext { get { return context as ShopContext; } }

        public void AddToCart(string userId, int productId, int quantity)
        {
                var getCart = ShopContext.Carts.Where(i => i.UserId == userId && i.ProductId == productId).FirstOrDefault();

                if (getCart == null)
                {
                    var cart = new Cart()
                    {
                        UserId = userId,
                        ProductId = productId,
                        Quantity = quantity
                    };

                    ShopContext.Carts.Add(cart);
                } else {
                    getCart.Quantity += quantity;
                }
        }

        public void CartEdit(string userId, int productId, int quantity)
        {            
            var cart = ShopContext.Carts.Where(i => i.UserId == userId && i.ProductId == productId).FirstOrDefault();

            if(cart == null)
            {
                Console.WriteLine("Hata ! Ürün Adet Sayısı güncellenemedi");
            }

            cart.Quantity = quantity;

            ShopContext.Carts.Update(cart);
        }

        public void CreateProductAndCategory(Product entity, int[] categoryIds)
        {
            ShopContext.Set<Product>().Add(entity);

            entity.ProductCategories = categoryIds.Select(a => new ProductCategory() {
                ProductId = entity.ProductId,
                CategoryId = a
            }).ToList();
        }

        public void DeleteToCart(string userId, int productId)
        {
            var getCart = ShopContext.Carts.Where(i => i.UserId == userId && i.ProductId == productId);

            if (getCart == null)
            {
                Console.WriteLine("Hata böyle bir kart yok !");
            }

            var cart2 = new Cart()
            {
                Id = getCart.Select(i => i.Id).FirstOrDefault(),
                UserId = userId,
                ProductId = productId
            };

            ShopContext.Carts.Remove(cart2);
        }

        public Product GetByIdWithCategories(int id)
        {
            return ShopContext.Products
                        .Where(i => i.ProductId == id)
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .FirstOrDefault();
        }

        public List<Cart> GetCart(string userId)
        {
            return ShopContext.Carts
                        .Where(i => i.UserId == userId)
                        .Include(i => i.Product)
                        .ToList();
        }

        public List<Product> GetCategoryProducts(string url,int page,int pageSize)
        {
            var products = ShopContext.Products.Where(i => i.IsApproved).AsQueryable();
            if (url != null)
            {
                products = ShopContext.Products
                                .Where(i => i.IsApproved)
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category) 
                                .Where(i => i.ProductCategories.Any(i => i.Category.Url == url));
            }

            return products.OrderByDescending(i => i.Rating).Skip((page-1)*pageSize).Take(pageSize).ToList(); // Skip ürünleri öteler. Take ise öteledikten sonra kaç tane ürün alacağını gösterir
                                                                // 1=1 = 0*1 = 0 (Baştaki pageSize kadar ürünü al)
        }

        public int GetCountByCategory(string category)
        {
            var products = ShopContext.Products.Where(i => i.IsApproved).AsQueryable();
            if (category != null)
            {
                products = ShopContext.Products
                                .Where(i => i.IsApproved)
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category) 
                                .Where(i => i.ProductCategories.Any(i => i.Category.Url == category));
            }

            return products.Count();
        }

        public List<Product> GetHomeProducts()
        {
            var product = ShopContext.Products.Where(i => i.IsHome == true && i.IsApproved == true).OrderByDescending(i => i.Rating).ToList();

            return product;
        }

        public bool GetImageName(string fileLocation, string imageName, int imageSize)
        {
            var applicationFile = Directory.GetFiles(fileLocation,"*").Select(i => Path.GetFileName(i));
            foreach (var item in applicationFile)
            {
                if (imageName == item)
                {
                    return false;
                }
            }

            return true;
        }

        public Product GetProductDetails(string url)
        {
                return ShopContext.Products
                                .Where(i => i.IsApproved && i.Url == url)
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .FirstOrDefault();     
        }

        public int GetSearchCount(string url)
        {
            var products = ShopContext.Products.Where(i => i.IsApproved == true && (i.Name.ToLower().Contains(url.ToLower()) || i.Description.ToLower().Contains(url.ToLower()) || i.Name.ToUpper().Contains(url.ToUpper()) || i.Description.ToUpper().Contains(url.ToUpper())) ).ToList();

            return products.Count();
        }

        public List<Product> GetSearchProduct(string url,int page,int pageSize)
        {
            var products = ShopContext.Products.Where(i => i.IsApproved == true && (i.Name.ToLower().Contains(url.ToLower()) || i.Description.ToLower().Contains(url.ToLower()) || i.Name.ToUpper().Contains(url.ToUpper()) || i.Description.ToUpper().Contains(url.ToUpper())) ).OrderByDescending(i => i.Rating).ToList();

            return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
                            // 1=1 = 0*1 = 0 (Baştaki pageSize kadar ürünü al)
        }

        public void ProductCategoryUpdate(Product entity, int[] categoryIds)
        {
            var product = ShopContext.Products
                                .Include(i => i.ProductCategories)
                                .FirstOrDefault(i => i.ProductId == entity.ProductId);

            if (product != null)
            {
                product.Name = entity.Name;
                product.Url = entity.Url;
                product.Price = entity.Price;
                product.Rating = entity.Rating;
                product.Stock = entity.Stock;
                product.Description = entity.Description;
                product.ImageUrl = entity.ImageUrl;
                product.IsHome = entity.IsHome;
                product.IsApproved = entity.IsApproved;
            }

            product.ProductCategories = categoryIds.Select(a => new ProductCategory() {
                ProductId = product.ProductId,
                CategoryId = a
            }).ToList();
        }
    }
}