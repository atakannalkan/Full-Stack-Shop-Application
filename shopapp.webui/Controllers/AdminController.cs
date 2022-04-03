using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.EmailServices.SMTP;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IEmailSender _emailSender;
        public AdminController(IProductService productService, ICategoryService categoryService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,IEmailSender emailSender)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }
        // Admin Panel
        public IActionResult AdminPanel()
        {
            var model = new AdminPanelModel()
            {
                Products = _productService.GetAll(),
                Categories = _categoryService.GetAll(),
                Users = _userManager.Users,
                Roles = _roleManager.Roles
            };
            
            return View(model);
        }

        // Product
        public IActionResult ProductList()
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetAll()
            };
            return View(productViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ProductCreate()
        {
            ViewBag.AllCategory = _categoryService.GetAll();
            ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
            
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model, int[] categoryIds, IFormFile file,string selectedImageUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllCategory = _categoryService.GetAll();
                ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                ViewBag.ReturnCategoryIds = categoryIds;
                return View(model);
            }

            model.Rating = model.Rating.Replace(".",",");
            var entity = new Product()
            {
                Name = model.Name,
                Url = model.Url,
                Price = model.Price,
                Rating = Double.Parse(model.Rating),
                Stock = model.Stock,
                Description = model.Description,
                ImageUrl = selectedImageUrl!=null?selectedImageUrl:null,
                IsHome = model.IsHome,
                IsApproved = model.IsApproved,
                DateAdded = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
            };

            var path = ""; // Path objesini global tanımladım.

            if (file != null)
            {
                if (_productService.GetImageName("wwwroot/img/", file.FileName, (int)file.Length) == false)
                {
                    ModelState.AddModelError("",_productService.ErrorMessage);
                    ViewBag.AllCategory = _categoryService.GetAll();
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img","*.jpg").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    ViewBag.ReturnCategoryIds = categoryIds;
                    return View(model);
                }

                var extension = Path.GetExtension(file.FileName); // => Resmin uzantısını aldık.
                if (extension != ".jpg")
                {
                    ModelState.AddModelError("","Image extension must be jpg !");
                    ViewBag.AllCategory = _categoryService.GetAll();
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    ViewBag.ReturnCategoryIds = categoryIds;
                    return View(model);
                }

                //var randomName = string.Format($"{Guid.NewGuid()}{extension}"); // => NewGuid ile benzersiz bir token oluşturduk
                entity.ImageUrl = file.FileName;
                path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img",file.FileName);                

                // Yüklemeyi aşağıda yaptım
            } else {
                if(selectedImageUrl == null)
                {
                    ModelState.AddModelError("","A picture must be selected !");
                    ViewBag.AllCategory = _categoryService.GetAll();
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    ViewBag.ReturnCategoryIds = categoryIds;
                    return View(model);
                }
            }

            if (_productService.CreateProductAndCategory(entity, categoryIds) == false) // Eğer Business katmanından bir hata gelirse..
            {
                ModelState.AddModelError("",_productService.ErrorMessage);
                ViewBag.AllCategory = _categoryService.GetAll();
                ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                ViewBag.ReturnCategoryIds = categoryIds;
                return View(model);
            } else if(file != null)
            {
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            
            CreateMessage(entity.ProductId.ToString(),"The product with id has been successfully created !","success");
            return RedirectToAction("ProductList");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ProductEdit(int id)
        {
            var product = _productService.GetByIdWithCategories(id);
            if (product == null)
            {
                CreateMessage(id.ToString(),"The product with id not available !","danger");
                return RedirectToAction("ProductList");
            }

            var model = new ProductModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Url = product.Url,
                Price = product.Price,
                Rating = product.Rating.ToString().Replace(",","."),
                Stock = product.Stock,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                IsHome = product.IsHome,
                IsApproved = product.IsApproved,
                DateAdded = product.DateAdded,
                SelectedCategories = product.ProductCategories.Select(i => i.Category).ToList()
            };
            ViewBag.AllCategories = _categoryService.GetAll();
            ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img","*.jpg").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductModel model, int[] categoryIds, IFormFile file,string selectedImageUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllCategories = _categoryService.GetAll();
                ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img","*.jpg").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                return View(model);
            }

            var product = _productService.GetById(model.ProductId);

            if (product == null)
            {
                CreateMessage("","There is no such product !","danger");
                ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img","*.jpg").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                return RedirectToAction("ProductList");
            }

            model.Rating = model.Rating.Replace(".",",");

            product.Name = model.Name;
            product.Url = model.Url;
            product.Price = model.Price;
            product.Rating = double.Parse(model.Rating);
            product.Stock = model.Stock;
            product.Description = model.Description;
            product.ImageUrl = selectedImageUrl!=null?selectedImageUrl:model.ImageUrl;
            product.IsHome = model.IsHome;
            product.IsApproved = model.IsApproved;            

            var path = ""; // Path objesini global tanımladım.
            if (file != null)
            {
                if (_productService.GetImageName("wwwroot/img/", file.FileName, (int)file.Length) == false)
                {
                    ViewBag.AllCategories = _categoryService.GetAll();
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img","*.jpg").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    ModelState.AddModelError("",_productService.ErrorMessage);
                    return View(model);
                }

                var extension = Path.GetExtension(file.FileName); // => Resmin uzantısını aldık.
                if (extension != ".jpg")
                {
                    ModelState.AddModelError("","Image extension must be jpg !");
                    ViewBag.AllCategories = _categoryService.GetAll();
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    return View(model);
                }
                //var randomName = string.Format($"{Guid.NewGuid()}{extension}"); // => NewGuid ile benzersiz bir token oluşturduk
                product.ImageUrl = file.FileName;
                path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img",file.FileName);     

                // Yüklemeyi aşağıda yaptım.           
            }

            if (_productService.ProductCategoryUpdate(product, categoryIds) == false) // Eğer Business katmanından bir hata gelirse..
            {                
                ViewBag.AllCategories = _categoryService.GetAll();
                ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img","*.jpg").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                ModelState.AddModelError("",_productService.ErrorMessage);
                return View(model);
            } else if (file != null)
            {
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            // Ve güncelleme işlemi
            _productService.ProductCategoryUpdate(product, categoryIds);
            CreateMessage(product.ProductId.ToString(),"The product with id has been successfully updated !","success"); 
           
            return RedirectToAction("ProductList");
        }

        [Authorize(Roles = "admin")]
        public IActionResult ProductDelete(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                CreateMessage(id.ToString(),"The product with id not available !","danger");
                return RedirectToAction("ProductList");
            }

            _productService.Delete(product);
            CreateMessage(id.ToString(),"The product with id has been successfully deleted !","success");

            return RedirectToAction("ProductList");
        }

        // Category 
        public IActionResult CategoryList()
        {
            var categoryViewModel = new CategoryListModel()
            {
                Categories = _categoryService.GetAll()
            };
            return View(categoryViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
                Url = model.Url
            };

            if (_categoryService.Create(entity) == false)
            {
                ModelState.AddModelError("",_categoryService.ErrorMessage);
                return View(model);
            }

            CreateMessage(entity.CategoryId.ToString(),"The category with id has been successfully created !","success");
            return RedirectToAction("CategoryList");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CategoryEdit(int id)
        {
            var entity = _categoryService.GetByIdProducts(id);

            if (entity == null)
            {
                CreateMessage(id.ToString(),"The category with id not available !","danger");
                return RedirectToAction("CategoryList");
            }

            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.ProductCategories.Select(i => i.Product).ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CategoryEdit(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.CategoryId);

            if (entity == null)
            {
                CreateMessage(model.CategoryId.ToString(),"The category with id not available !","danger");
                return View(model);
            }

            entity.CategoryId = model.CategoryId;
            entity.Name = model.Name;
            entity.Url = model.Url;

            _categoryService.Update(entity);
            CreateMessage(entity.CategoryId.ToString(),"The category with id has been successfully updated !","success");
            return RedirectToAction("CategoryList");
        }

        [Authorize(Roles = "admin")]
        public IActionResult CategoryDelete(int id)
        {
            var entity = _categoryService.GetById(id);

            if (entity == null)
            {
                CreateMessage(id.ToString(),"The category with id not available !","danger");
                return RedirectToAction("CategoryList");
            }

            _categoryService.Delete(entity);
            CreateMessage(entity.CategoryId.ToString(),"The category with id has been successfully deleted !","success");

            return RedirectToAction("CategoryList");
        }

        [Authorize(Roles = "admin")]
        [HttpPost] 
        public IActionResult DeleteFromCategory(int productId, int categoryId)
        {
            Console.WriteLine("ProductId: "+productId+" CategoryId: "+categoryId);
            _categoryService.DeleteProductByCategory(productId, categoryId);
            CreateMessage(productId.ToString(),"The product with id has been successfully deleted from the category !","success");

            return Redirect("/admin/category/edit/"+categoryId);
        }

        // Users
        public IActionResult UserList()
        {
            return View(_userManager.Users.OrderBy(i => i.DateTime).ToList());
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult UserCreate()
        {
            ViewBag.AllRoles = _roleManager.Roles;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UserCreate(UserCreateModel model, string[] selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllRoles = _roleManager.Roles;
                return View(model);
            }

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed,
                CopyPassword = model.Password,
            };

            var result = await _userManager.CreateAsync(user,model.Password);

            if (result.Succeeded)
            {
                if (selectedRoles.Count() == 0)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                } else {
                    foreach (var addRole in selectedRoles.ToList())
                    {
                        var addRoles = await _userManager.AddToRoleAsync(user, addRole.ToString());
                    }
                }
                if (user.EmailConfirmed == false)
                {
                    var myToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail","Account",new {
                        userId = user.Id,
                        token = myToken
                    });
                    // Send Email            
                    await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi doğrulayın",$"<table width=100%><tbody><tr><td align=center style='padding-bottom: 50px;'><h3 style='margin-bottom: 30px; font-family: sans-serif; font-size: 23px; font-weight: bold; color: rgb(72, 72, 72);'> <img src='https://imgyukle.com/f/2022/01/06/oYCayj.png' width=35 > Atakan Alkan Alışveriş Uygulaması</h3><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 26px; font-weight: bold;'>Merhaba {user.FirstName} {user.LastName}</h4></td></tr><tr><td align=center style='border: 1px solid rgb(209, 209, 209); padding: 10px;'><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 24px; font-weight: bold;'> <img src='https://imgyukle.com/f/2021/12/22/oozA8v.png' width=40> E-posta adresinizi doğrulayın</h4><p style='padding-bottom: 10px; font-family: sans-serif; font-size: 16px; font-weight: normal; color: rgb(72, 72, 72);'>Bu e-posta adresinin aktif ve size ait olduğunu doğrulamak için lütfen aşağıdaki <b>Hesabı Doğrula</b> butonuna tıklayın.</p><a href='https://localhost:5001{url}'><img src='https://n11scdn3.akamaized.net/a1/org/20/01/09/67/70/56/88/16/45/85/51/30/05202063912658290578.png' width=220 style='padding-top: 10px;'></a></td></tr></tbody></table>");
                }

                CreateMessage("","Account created successfully !","success");
                return RedirectToAction("UserList");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("",item.Description);
            }
            ViewBag.AllRoles = _roleManager.Roles;
            return View(model);            
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                CreateMessage(id.ToString(),"The user with id not available !","danger");
                return RedirectToAction("UserList");
            }

            var model = new UserModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                DateTime = user.DateTime,                
                EmailConfirmed = user.EmailConfirmed,
                CopyPassword = user.CopyPassword,
                SelectedRoles = await _userManager.GetRolesAsync(user)
            };
            ViewBag.AllRoles = _roleManager.Roles;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserModel model, string[] selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                CreateMessage(model.UserId.ToString(),"The user with id not available !","danger");
                return RedirectToAction("UserList");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.DateTime = model.DateTime;
            user.Email = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;
            
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Bu benim yazdığım kod ancak SqlServer'da hata aldığım için kaldırdım.
                // foreach (var allRole in _roleManager.Roles) // Bütün rolleri aldım.
                // {
                //     var role = await _userManager.IsInRoleAsync(user,allRole.ToString());
                //     if (role)
                //     {
                //         var deleteRoles = await _userManager.RemoveFromRoleAsync(user, allRole.ToString()); // Bütün rolleri kullanıcıdan sildim.
                //     }                    
                // }
                // foreach (var addRole in selectedRoles)
                // {
                //     var addRoles = await _userManager.AddToRoleAsync(user, addRole);
                // }

                var userRoles = await _userManager.GetRolesAsync(user);
                selectedRoles = selectedRoles?? new string[]{};
                
                foreach (var item in selectedRoles.Except(userRoles).ToArray<string>())
                {
                    Console.WriteLine(item);
                }

                await _userManager.AddToRolesAsync(user,selectedRoles.Except(userRoles).ToArray<string>());
                await _userManager.RemoveFromRolesAsync(user,userRoles.Except(selectedRoles).ToArray<string>());
                CreateMessage(user.Id.ToString(),"The user with id has been successfully updated !","success");
                return RedirectToAction("UserList");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("",item.Description);
            }

            return RedirectToAction("UserList");
        }

        [Authorize(Roles = "admin")]
        // [HttpPost]
        public async Task<IActionResult> UserDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);            

            if (user == null)
            {
                CreateMessage(id.ToString(),"The user with id not available ","danger");
                return RedirectToAction("UserList");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                CreateMessage(id.ToString(),"The user with id has been successfully deleted !","success");
                return RedirectToAction("UserList");
            }

            foreach (var item in result.Errors)
            {
                CreateMessage("",item.Description, "danger");
            }
            return RedirectToAction("UserList");
        }

        // Roles
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        [Authorize(Roles = "admin")]        
        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));

            if (result.Succeeded)
            {
                CreateMessage("","Role successfully created !","success");
                return RedirectToAction("RoleList");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("",item.Description);
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                CreateMessage(id.ToString(),"The role with id not available !","danger");
                return RedirectToAction("RoleList");
            }

            var members = new List<User>();
            var nonMembers = new List<User>();

            foreach (var user in _userManager.Users.ToList())
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name)?members:nonMembers;
                list.Add(user);
            }

            var model = new RoleDetailsModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            foreach (var userId in model.AddToRole ?? new string[]{})
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    CreateMessage(userId.ToString(),"The role with id not available !","danger");
                    return Redirect("/admin/role/edit/"+model.RoleId);
                }

                var result = await _userManager.AddToRoleAsync(user,model.RoleName);
                if (result.Succeeded)
                {
                    CreateMessage("","Transaction Successful !","success");                    
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }

            foreach (var userId in model.DeleteToRole ?? new string[] {})
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    CreateMessage(userId.ToString(),"The role with id not available !","danger");
                    return Redirect("/admin/role/edit/"+model.RoleId);
                }

                var result = await _userManager.RemoveFromRoleAsync(user,model.RoleName);
                if (result.Succeeded)
                {
                    CreateMessage("","Transaction Successful !","success");
                }

                foreach (var item in result.Errors)
                {
                    CreateMessage("",item.Description,"danger");
                }
            }
            return Redirect("/admin/role/edit/"+model.RoleId);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RoleDelete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                CreateMessage(role.Id.ToString(),"The role with id not available !","danger");
                return RedirectToAction("RoleList");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                CreateMessage(id.ToString(),"The role with id has been successfully deleted !","success");
                return RedirectToAction("RoleList");
            }

            foreach (var item in result.Errors)
            {
                CreateMessage("",item.Description,"danger");
            }
            return RedirectToAction("RoleList");
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