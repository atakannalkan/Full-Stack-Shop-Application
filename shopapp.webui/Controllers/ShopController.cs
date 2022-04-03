using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        private  ISettingsService _settingsService;
        private UserManager<User> _userManager;
        public ShopController(IProductService productService, ISettingsService settingsService,UserManager<User> userManager)
        {
            _productService = productService;
            _settingsService = settingsService;
            _userManager = userManager;
        }

        public IActionResult Details(string url)
        {
            Product product = _productService.GetProductDetails(url);

            if (product != null)
            {
                var model = new ProductDetailsModel() {
                    Product = product,
                    Categories = product.ProductCategories.Select(i => i.Category).ToList(),
                };
                return View(model);
            }

            return NotFound();
        }

        public IActionResult CategoryList(string url, int page = 1)
        {
            int pageSize = _settingsService.GetArrangement();
            var model = new ProductListViewModel()
            {
                Products = _productService.GetCategoryProducts(url,page,pageSize),
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(url),
                    PageSize = pageSize,
                    CurrentPage = page,
                    CurrentCategory = url
                }                
            };
            return View(model);
        }

        public IActionResult SearchProduct(string url, int page = 1)
        {
            ViewBag.SearchProduct = url;
            int pageSize = _settingsService.GetArrangement();

            if (string.IsNullOrEmpty(url))
            {
                return NoContent();
            }
            var product = _productService.GetSearchProduct(url,page,pageSize);
            
            var model = new ProductListViewModel()
            {
                Products = product,
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetSearchCount(url),
                    PageSize = pageSize,
                    CurrentPage = page,
                    CurrentCategory = null
                }
            };

            return View(model);
        }

        public IActionResult CartList()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                CreateMessage("Hata !","Ürün sepetini görüntülemek için giriş yapmanız gerekir !","danger");
                return Redirect("/account/login");
            }

            var userId = _userManager.GetUserId(User);

            var model = new CartModel()
            {
                CartItem = _productService.GetCart(userId)
            };

            return View(model);
        }

        public IActionResult AddToCart(int productId, int quantity)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                CreateMessage("Hata !","Ürünü sepete eklemek için giriş yapmanız gerekir !","danger");
                return Redirect("/account/login");
            }

            var userId = _userManager.GetUserId(User);
            var product = _productService.GetById(productId);

            if (product == null)
            {
                // Hata Mesajı
            }

            if(_productService.AddToCart(userId, productId, quantity) == false)
            {
                CreateMessage("Hata !",_productService.ErrorMessage,"danger");
                return RedirectToAction("CartList");
            }

            CreateMessage(product.Name,"Adlı ürün sepetinize başarıyla eklendi !","success");            
            return RedirectToAction("CartList");
        }

        public IActionResult DeleteToCart(int productId, string productName)
        {
            var userId = _userManager.GetUserId(User);

            _productService.DeleteToCart(userId, productId);

            CreateMessage(productName," Adlı ürün sepetinizden başarıyla silindi !","warning");

            return RedirectToAction("CartList");
        }

        [HttpPost]
        public IActionResult CartEdit(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);

            if(_productService.CartEdit(userId, productId, quantity) == false)
            {
                CreateMessage("Hata !",_productService.ErrorMessage,"danger");
                return RedirectToAction("CartList");
            }          

            // Create Message

            return RedirectToAction("CartList");
        }

        // Send Message
        public void CreateMessage(string name, string message, string alertType)
        {
            var msg = new AlertMessage()
            {
                Name = name,
                Message = message,
                AlertType = alertType,
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
        }
    }
}