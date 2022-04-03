using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class SettingsController : Controller
    {
        private ISettingsService _settingsService;
        private IProductService _productService;
        public SettingsController(ISettingsService settingsService, IProductService productService)
        {
            _settingsService = settingsService;
            _productService = productService;
        }

        // Slider
        public IActionResult Index()
        {
            var sliderModel = new SliderViewModel()
            {
                Sliders = _settingsService.GetSlider()
            };
            return View(sliderModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult SliderCreate()
        {            
            ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/slider","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
            return View();
        }

        [Authorize(Roles = "admin")]        
        [HttpPost]
        public async Task<IActionResult> SliderCreate(Slider model, IFormFile file, string selectedImageUrl)
        {
            var entity = new Slider()
            {
                ImageUrl = selectedImageUrl,
                Order = model.Order,
                Active = model.Active
            };

            if(file != null)
            {
                if(_productService.GetImageName("wwwroot/img/slider/", file.FileName, (int)file.Length) == false)
                {
                    ModelState.AddModelError("",_productService.ErrorMessage);
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/slider","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    return View(model);
                }

                var extension = Path.GetExtension(file.FileName); // => Resmin uzantısı
                if (extension != ".png")
                {
                    ModelState.AddModelError("","Image extension must be png !");
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/slider","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    return View(model);
                }
                
                //var randomName = string.Format($"{Guid.NewGuid()}{extension}"); // => NewGuid ile benzersiz bir token oluşturduk
                entity.ImageUrl = file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img\\slider",file.FileName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            } else {
                if(selectedImageUrl == null)
                {
                    ModelState.AddModelError("","A picture must be selected '");
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/slider","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    return View(model);
                }
            }

            _settingsService.Create(entity);
            CreateMessage(entity.Id.ToString(),"The slider with id has been successfully created !","success");

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult SliderEdit(int id)
        {
            var entity = _settingsService.GetById(id);

            if (entity == null)
            {
                CreateMessage(id.ToString(),"The slider with id not available !","danger");
                return RedirectToAction("Index");
            }

            var model = new SliderModel()
            {
                Id = entity.Id,
                ImageUrl = entity.ImageUrl,
                Order = entity.Order,
                Active = entity.Active
            };
            ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/slider","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> SliderEdit(SliderModel model,IFormFile file, string selectedImageUrl)
        {
            var entity = _settingsService.GetById(model.Id);

            if (entity == null)
            {
                CreateMessage(model.Id.ToString(),"The slider with id not available !","danger");
                return View(model);
            }

            entity.ImageUrl = selectedImageUrl;
            entity.Order = model.Order;
            entity.Active = model.Active;

            if(file != null)
            {
                if(_productService.GetImageName("wwwroot/img/slider/", file.FileName, (int)file.Length) == false)
                {
                    ModelState.AddModelError("",_productService.ErrorMessage);
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/slider","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    return View(model);
                }

                var extension = Path.GetExtension(file.FileName); // => Resmin uzantısı
                if (extension != ".png")
                {
                    ModelState.AddModelError("","Image extension must be png !");
                    ViewBag.AllImageUrl = Directory.GetFiles("wwwroot/img/slider","*").Select(i => Path.GetFileName(i)); // GetFiles: İlgili klasör içindeki dosyaları alır - GetFileName: ana "wwwroot/img/1.jpg" şeklinde gelen resmin sadece ismini alır => "1.jpg"
                    return View(model);
                }
                
                //var randomName = string.Format($"{Guid.NewGuid()}{extension}"); // => NewGuid ile benzersiz bir token oluşturduk
                entity.ImageUrl = file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img\\slider",file.FileName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            _settingsService.Update(entity);
            CreateMessage(model.Id.ToString(),"The slider with id has been successfully updated !","success");

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult SliderDelete(int id)
        {
            var entity = _settingsService.GetById(id);

            if (entity == null)
            {
                CreateMessage(id.ToString(),"The slider with id not available !","danger");
                return RedirectToAction("Index");
            }

            _settingsService.Delete(entity);
            CreateMessage(id.ToString(),"The slider with id has been successfully deleted !","success");

            return RedirectToAction("Index");
        }        

        // Arrangement => Sıralama
        [HttpGet]
        public IActionResult Arrangement()
        {
            var model = new Arrangement()
            {
                Content = _settingsService.GetArrangement()
            };
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Arrangement(Arrangement model)
        {
            model.Id = 1;
            _settingsService.ArngmntUpdate(model);
            CreateMessage(model.Id.ToString(),"The arrangement with id has been successfully updated !","success");

            // Not!: Güncelleme kodları biraz saçma oldu ancak sadece 1 veri güncellediğim için yapıya çok önem göstermedim.

            return View();
        }

        public IActionResult Files()
        {
            var model = new ImageViewModel()
            {
                IndexImageUrl = Directory.GetFiles("wwwroot/img/","*").Select(i => Path.GetFileName(i)),
                SliderImageUrl = Directory.GetFiles("wwwroot/img/slider/","*").Select(i => Path.GetFileName(i)),
                SvgImageUrl = Directory.GetFiles("wwwroot/img/svg/","*").Select(i => Path.GetFileName(i)),
                IconImageUrl = Directory.GetFiles("wwwroot/img/icon/","*").Select(i => Path.GetFileName(i))
            };
            return View(model);
        }
        
        [Authorize(Roles = "admin")]        
        [HttpPost]
        public IActionResult DeleteIndexImage(string fileLocation, string[] imageName)
        {
            if (imageName == null || fileLocation == null)
            {
                return NoContent();
            }

            string[] index = new string[imageName.Count()];
            for (int i = 0; i < imageName.Length; i++)
            {
                index[i] = Directory.GetFiles(fileLocation,imageName[i]).FirstOrDefault();
            }

            foreach (var item in index)
            {
                System.IO.File.Delete(item);                
            }

            CreateMessage(fileLocation,"Image(s) in file successfully deleted !","success");

            return RedirectToAction("Files");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddIndexImage(string fileLocation, IFormFile file)
        {
            if (file == null || fileLocation == null)
            {
                return NoContent();
            }

            if(_productService.GetImageName(fileLocation, file.FileName, (int)file.Length) == false)
            {
                CreateMessage("Error!",_productService.ErrorMessage,"danger");
                return RedirectToAction("Files"); 
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(),fileLocation,file.FileName);

            using (var stream = new FileStream(path,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            CreateMessage(fileLocation+"/"+file.FileName,"Image in file successfully created !","success");

            return RedirectToAction("Files");
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