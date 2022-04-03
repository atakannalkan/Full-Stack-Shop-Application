using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.webui.EmailServices.SMTP;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                Email = model.Email,
                CopyPassword = model.Password
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                
                // Send Mail
                var myToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail","Account",new {
                    userId = user.Id,
                    token = myToken
                });                
                await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi doğrulayın",$"<table width=100%><tbody><tr><td align=center style='padding-bottom: 50px;'><h3 style='margin-bottom: 30px; font-family: sans-serif; font-size: 23px; font-weight: bold; color: rgb(72, 72, 72);'> <img src='https://imgyukle.com/f/2022/01/06/oYCayj.png' width=35 > Atakan Alkan Alışveriş Uygulaması</h3><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 26px; font-weight: bold;'>Merhaba {user.FirstName} {user.LastName}</h4></td></tr><tr><td align=center style='border: 1px solid rgb(209, 209, 209); padding: 10px;'><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 24px; font-weight: bold;'> <img src='https://imgyukle.com/f/2022/01/26/oUMadn.png' width=40> E-posta adresinizi doğrulayın</h4><p style='padding-bottom: 10px; font-family: sans-serif; font-size: 16px; font-weight: normal; color: rgb(72, 72, 72);'>Bu e-posta adresinin aktif ve size ait olduğunu doğrulamak için lütfen aşağıdaki <b>Hesabı Doğrula</b> butonuna tıklayın.</p><a href='https://localhost:5001{url}'><img src='https://n11scdn3.akamaized.net/a1/org/20/01/09/67/70/56/88/16/45/85/51/30/05202063912658290578.png' width=220 style='padding-top: 10px;'></a></td></tr></tbody></table>");

                CreateMessage("","Hesabınız başarıyla oluşturuldu hemen giriş yapabilirsiniz !","success");
                return RedirectToAction("Login");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("",item.Description);
                Console.WriteLine("Kod: "+item.Code);
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var userEmail = await _userManager.FindByNameAsync(model.UsernameOrEmail);
            var userName = await _userManager.FindByEmailAsync(model.UsernameOrEmail);

            if (userName == null && userEmail == null)
            {
                ModelState.AddModelError("","Böyle bir kullanıcı yok !");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(userEmail==null?userName:userEmail,model.Password,model.RememberMe,true);
            
            if(result.IsLockedOut)
            {
                CreateMessage("DİKKAT !","5 defa yanlış parola girdiğiniz için hesabınız 1 dakikalığına kilitlenmiştir","danger");
                Console.WriteLine(_userManager.GetLockoutEndDateAsync(userEmail==null?userName:userEmail));
                return View(model);
            }
            
            if (result.Succeeded)
            {
                // CreateMessage("","Başarılı bir şekilde giriş yapıldı !","success");

                var msg = new AlertMessage()
                {
                    Name = "",
                    Message = "Başarılı bir şekilde giriş yapıldı ",
                    AlertType = "success",
                };
                TempData["accountMessage"] = JsonConvert.SerializeObject(msg);
                return Redirect("/");                
            }

            ModelState.AddModelError("","Yanlış kullanıcı adı veya parola");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // CreateMessage("","Güvenli bir şekilde çıkış yapıldı !","warning");
            var msg = new AlertMessage()
            {
                Name = "",
                Message = "Güvenli bir şekilde çıkış yapıldı !",
                AlertType = "warning",
            };
            TempData["accountMessage"] = JsonConvert.SerializeObject(msg);

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (String.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("Email","'Email' alanı boş bırakılamaz !");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                ModelState.AddModelError("","Böyle bir E-Mail'e bağlı bir kullanıcı yok !");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword","Account",new {
                userId = user.Id,
                token = token
            });

            await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi doğrulayın",$"<table width=100%><tbody><tr><td align=center style='padding-bottom: 50px;'><h3 style='margin-bottom: 30px; font-family: sans-serif; font-size: 23px; font-weight: bold; color: rgb(72, 72, 72);'> <img src='https://imgyukle.com/f/2022/01/06/oYCayj.png' width=35 > Atakan Alkan Alışveriş Uygulaması</h3><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 26px; font-weight: bold;'>Merhaba {user.FirstName} {user.LastName}</h4></td></tr><tr><td align=center style='border: 1px solid rgb(209, 209, 209); padding: 10px;'><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 24px; font-weight: bold;'> <img src='https://imgyukle.com/f/2022/01/26/oUMadn.png' width=40> E-posta adresinizi doğrulayın</h4><p style='padding-bottom: 10px; font-family: sans-serif; font-size: 16px; font-weight: normal; color: rgb(72, 72, 72);'>Bu e-posta adresinin aktif ve size ait olduğunu doğrulamak için lütfen aşağıdaki <b>Hesabı Doğrula</b> butonuna tıklayın.</p><a href='https://localhost:5001{url}'><img src='https://n11scdn3.akamaized.net/a1/org/20/01/09/67/70/56/88/16/45/85/51/30/05202063912658290578.png' width=220 style='padding-top: 10px;'></a></td></tr></tbody></table>");

            CreateMessage("",$"Şifre yenileme bağlantısı {Email} adresine başarıyla gönderildi !","success");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                CreateMessage("","Geçersiz Token !","danger");
                return View();
            }
            Console.WriteLine("User Id: "+userId);
            Console.WriteLine("Token: "+token);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                CreateMessage("","Böyle bir kullanıcı yok !","danger");
                return RedirectToAction("Login");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                user.CopyPassword = model.Password;
                await _userManager.UpdateAsync(user); // CopyPassword'u güncelledim.

                CreateMessage("","Şifreniz başarılı bir şekilde güncellendi, hemen giriş yapabilirsiniz !","success");
                return RedirectToAction("Login");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("",item.Description);
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> UserAccount()
        {
            var user = _userManager.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault(); // Veritabanımdaki kullanıcı adı ile giriş yapanın kullanıcı adı aynı ise verileri gönderdim.
        
            if (user == null)
            {
                CreateMessage("Hata !","Bu kullanıcı adı ile daha önce hesap oluşturulmamış, hatanın devam etmesi halinde lütfen site yöneticisine başvurun !","danger");
                return View();
            }

            var model = new UserViewModel()
            {
                User = user,
                UserRoles = await _userManager.GetRolesAsync(user)
            };                   

            return View(model);
        }

        // Bu metod hesabım sekmesindeki kullanıcının maili onaylı değilse, mail göndermek için tasarlandı.
        [HttpPost]
        public async Task<IActionResult> UserAccountConfirmEmail()
        {
            var user = _userManager.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();

            if (user == null)
            {
                CreateMessage("Hata !","giriş yapılan kullanıcı bulunamadı, hatanın devam etmesi halinde site yöneticisi ile iletişime geçiniz !","danger");
            }

            var myToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("ConfirmEmail","Account",new {
                userId = user.Id,
                token = myToken
            });
            // Send Email
            // await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi doğrulayın", $"<table width=100%> <tbody align=center> <tr><td align=center> <h2>Merhaba {user.FirstName}</h2> </td></tr> </tbody> </table> Email hesabınızı onaylaman için <a href='https://localhost:5001{url}'>buraya</a> tıklayınız");
            await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi doğrulayın",$"<table width=100%><tbody><tr><td align=center style='padding-bottom: 50px;'><h3 style='margin-bottom: 30px; font-family: sans-serif; font-size: 23px; font-weight: bold; color: rgb(72, 72, 72);'> <img src='https://imgyukle.com/f/2022/01/06/oYCayj.png' width=35 > Atakan Alkan Alışveriş Uygulaması</h3><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 26px; font-weight: bold;'>Merhaba {user.FirstName} {user.LastName}</h4></td></tr><tr><td align=center style='border: 1px solid rgb(209, 209, 209); padding: 10px;'><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 24px; font-weight: bold;'> <img src='https://imgyukle.com/f/2022/01/26/oUMadn.png' width=40> E-posta adresinizi doğrulayın</h4><p style='padding-bottom: 10px; font-family: sans-serif; font-size: 16px; font-weight: normal; color: rgb(72, 72, 72);'>Bu e-posta adresinin aktif ve size ait olduğunu doğrulamak için lütfen aşağıdaki <b>Hesabı Doğrula</b> butonuna tıklayın.</p><a href='https://localhost:5001{url}'><img src='https://n11scdn3.akamaized.net/a1/org/20/01/09/67/70/56/88/16/45/85/51/30/05202063912658290578.png' width=220 style='padding-top: 10px;'></a></td></tr></tbody></table>");
            
            CreateMessage("İşlem Başarılı !","Hesabınıza onay maili gönderilmiştir !","success");
            return RedirectToAction("UserAccount");
        }

        public  async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                CreateMessage("Hata, Geçersiz Token !","Böyle bir token bulunamadı, hatanın devam etmesi halinde site yöneticisi ile iletişime geçiniz !","danger");
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                CreateMessage("Hata, Kullanıcı Bulunamadı !","Böyle bir kullanıcı bulunamadı, hatanın devam etmesi halinde site yöneticisi ile iletişime geçiniz !","danger");
                return View();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    CreateMessage("Hata, E-mail Onaylanamadı",$"Email onaylanmadı, hatanın devam etmesi halinde site yöneticisi ile iletişime geçiniz ! Hata Açıklaması: '{item.Description}' ","danger");
                }                
                return View();
            }

            CreateMessage("İşlem başarılı, Hesabınız Onaylandı !","Email hesabınız başarılı bir şekilde onaylandı. Güvenli bir şekilde Alışverişe devam edebilirsiniz !","success");
            return View();
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