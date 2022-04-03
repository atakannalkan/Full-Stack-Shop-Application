using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;

namespace shopapp.webui.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;
        private IProductService _productService;
        public CategoryViewComponent(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.CategoryUrl = RouteData.Values["url"];
            ViewBag.CategoryAction = RouteData.Values["action"];

            var sayac = 0;
            for (int i = 1; i <= _categoryService.GetAll().Count(); i++)
            {
                adim1:
                int? sonuc = _categoryService.GetCategoryCount(++sayac);
                if (sonuc == null)
                {
                    goto adim1;
                }
                TempData["CategoryCount"+sayac] = sonuc;
            }

            return View(_categoryService.GetAll());
        }
    }
}