using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.Identity;

namespace shopapp.webui.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private IProductService _productService;
        private UserManager<User> _userManager; 
        public CartViewComponent(IProductService productService, UserManager<User> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            ViewBag.UserCartCount = _productService.GetCart(userId).Count();

            return View();
        }
    }
}