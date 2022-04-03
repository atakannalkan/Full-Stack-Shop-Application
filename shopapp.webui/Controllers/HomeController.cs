using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private ISettingsService _settingsService;
        private UserManager<User> _userManager; 
        public HomeController(IProductService productService, ICategoryService categoryService, ISettingsService settingsService, UserManager<User> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _settingsService = settingsService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = new ProductListViewModel()
            {
                Products = _productService.GetHomeProducts(),
                Sliders = _settingsService.GetSlider()
            };
            var userId = _userManager.GetUserId(User);

            return View(model);            
        }

        // Api katmanındaki bilgilerin çekilmesi.
        public async Task<IActionResult> GetProductsFromRestApi()
        {
            var products = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:4000/api/products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync(); // Gelen cevabı string ile okuduk.
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse); // JSON bilgiyi DeSerialize ettik, yani String'e çevirdik
                }
            }
            return View(products);
        }
    }
}