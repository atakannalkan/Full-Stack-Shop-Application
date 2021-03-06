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
                await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi do??rulay??n",$"<table width=100%><tbody><tr><td align=center style='padding-bottom: 50px;'><h3 style='margin-bottom: 30px; font-family: sans-serif; font-size: 23px; font-weight: bold; color: rgb(72, 72, 72);'> <img src='https://imgyukle.com/f/2022/01/06/oYCayj.png' width=35 > Atakan Alkan Al????veri?? Uygulamas??</h3><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 26px; font-weight: bold;'>Merhaba {user.FirstName} {user.LastName}</h4></td></tr><tr><td align=center style='border: 1px solid rgb(209, 209, 209); padding: 10px;'><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 24px; font-weight: bold;'> <img src='https://imgyukle.com/f/2022/01/26/oUMadn.png' width=40> E-posta adresinizi do??rulay??n</h4><p style='padding-bottom: 10px; font-family: sans-serif; font-size: 16px; font-weight: normal; color: rgb(72, 72, 72);'>Bu e-posta adresinin aktif ve size ait oldu??unu do??rulamak i??in l??tfen a??a????daki <b>Hesab?? Do??rula</b> butonuna t??klay??n.</p><a href='https://localhost:5001{url}'><img src='https://n11scdn3.akamaized.net/a1/org/20/01/09/67/70/56/88/16/45/85/51/30/05202063912658290578.png' width=220 style='padding-top: 10px;'></a></td></tr></tbody></table>");

                CreateMessage("","Hesab??n??z ba??ar??yla olu??turuldu hemen giri?? yapabilirsiniz !","success");
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
                ModelState.AddModelError("","B??yle bir kullan??c?? yok !");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(userEmail==null?userName:userEmail,model.Password,model.RememberMe,true);
            
            if(result.IsLockedOut)
            {
                CreateMessage("D??KKAT !","5 defa yanl???? parola girdi??iniz i??in hesab??n??z 1 dakikal??????na kilitlenmi??tir","danger");
                Console.WriteLine(_userManager.GetLockoutEndDateAsync(userEmail==null?userName:userEmail));
                return View(model);
            }
            
            if (result.Succeeded)
            {
                // CreateMessage("","Ba??ar??l?? bir ??ekilde giri?? yap??ld?? !","success");

                var msg = new AlertMessage()
                {
                    Name = "",
                    Message = "Ba??ar??l?? bir ??ekilde giri?? yap??ld?? ",
                    AlertType = "success",
                };
                TempData["accountMessage"] = JsonConvert.SerializeObject(msg);
                return Redirect("/");                
            }

            ModelState.AddModelError("","Yanl???? kullan??c?? ad?? veya parola");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // CreateMessage("","G??venli bir ??ekilde ????k???? yap??ld?? !","warning");
            var msg = new AlertMessage()
            {
                Name = "",
                Message = "G??venli bir ??ekilde ????k???? yap??ld?? !",
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
                ModelState.AddModelError("Email","'Email' alan?? bo?? b??rak??lamaz !");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                ModelState.AddModelError("","B??yle bir E-Mail'e ba??l?? bir kullan??c?? yok !");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword","Account",new {
                userId = user.Id,
                token = token
            });

            await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi do??rulay??n",$"<table width=100%><tbody><tr><td align=center style='padding-bottom: 50px;'><h3 style='margin-bottom: 30px; font-family: sans-serif; font-size: 23px; font-weight: bold; color: rgb(72, 72, 72);'> <img src='https://imgyukle.com/f/2022/01/06/oYCayj.png' width=35 > Atakan Alkan Al????veri?? Uygulamas??</h3><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 26px; font-weight: bold;'>Merhaba {user.FirstName} {user.LastName}</h4></td></tr><tr><td align=center style='border: 1px solid rgb(209, 209, 209); padding: 10px;'><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 24px; font-weight: bold;'> <img src='https://imgyukle.com/f/2022/01/26/oUMadn.png' width=40> E-posta adresinizi do??rulay??n</h4><p style='padding-bottom: 10px; font-family: sans-serif; font-size: 16px; font-weight: normal; color: rgb(72, 72, 72);'>Bu e-posta adresinin aktif ve size ait oldu??unu do??rulamak i??in l??tfen a??a????daki <b>Hesab?? Do??rula</b> butonuna t??klay??n.</p><a href='https://localhost:5001{url}'><img src='https://n11scdn3.akamaized.net/a1/org/20/01/09/67/70/56/88/16/45/85/51/30/05202063912658290578.png' width=220 style='padding-top: 10px;'></a></td></tr></tbody></table>");

            CreateMessage("",$"??ifre yenileme ba??lant??s?? {Email} adresine ba??ar??yla g??nderildi !","success");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                CreateMessage("","Ge??ersiz Token !","danger");
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
                CreateMessage("","B??yle bir kullan??c?? yok !","danger");
                return RedirectToAction("Login");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                user.CopyPassword = model.Password;
                await _userManager.UpdateAsync(user); // CopyPassword'u g??ncelledim.

                CreateMessage("","??ifreniz ba??ar??l?? bir ??ekilde g??ncellendi, hemen giri?? yapabilirsiniz !","success");
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
            var user = _userManager.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault(); // Veritaban??mdaki kullan??c?? ad?? ile giri?? yapan??n kullan??c?? ad?? ayn?? ise verileri g??nderdim.
        
            if (user == null)
            {
                CreateMessage("Hata !","Bu kullan??c?? ad?? ile daha ??nce hesap olu??turulmam????, hatan??n devam etmesi halinde l??tfen site y??neticisine ba??vurun !","danger");
                return View();
            }

            var model = new UserViewModel()
            {
                User = user,
                UserRoles = await _userManager.GetRolesAsync(user)
            };                   

            return View(model);
        }

        // Bu metod hesab??m sekmesindeki kullan??c??n??n maili onayl?? de??ilse, mail g??ndermek i??in tasarland??.
        [HttpPost]
        public async Task<IActionResult> UserAccountConfirmEmail()
        {
            var user = _userManager.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();

            if (user == null)
            {
                CreateMessage("Hata !","giri?? yap??lan kullan??c?? bulunamad??, hatan??n devam etmesi halinde site y??neticisi ile ileti??ime ge??iniz !","danger");
            }

            var myToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("ConfirmEmail","Account",new {
                userId = user.Id,
                token = myToken
            });
            // Send Email
            // await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi do??rulay??n", $"<table width=100%> <tbody align=center> <tr><td align=center> <h2>Merhaba {user.FirstName}</h2> </td></tr> </tbody> </table> Email hesab??n??z?? onaylaman i??in <a href='https://localhost:5001{url}'>buraya</a> t??klay??n??z");
            await _emailSender.SendEmailAsync(user.Email,"E-posta adresinizi do??rulay??n",$"<table width=100%><tbody><tr><td align=center style='padding-bottom: 50px;'><h3 style='margin-bottom: 30px; font-family: sans-serif; font-size: 23px; font-weight: bold; color: rgb(72, 72, 72);'> <img src='https://imgyukle.com/f/2022/01/06/oYCayj.png' width=35 > Atakan Alkan Al????veri?? Uygulamas??</h3><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 26px; font-weight: bold;'>Merhaba {user.FirstName} {user.LastName}</h4></td></tr><tr><td align=center style='border: 1px solid rgb(209, 209, 209); padding: 10px;'><h4 style='color: rgb(72, 72, 72); font-family: sans-serif; font-size: 24px; font-weight: bold;'> <img src='https://imgyukle.com/f/2022/01/26/oUMadn.png' width=40> E-posta adresinizi do??rulay??n</h4><p style='padding-bottom: 10px; font-family: sans-serif; font-size: 16px; font-weight: normal; color: rgb(72, 72, 72);'>Bu e-posta adresinin aktif ve size ait oldu??unu do??rulamak i??in l??tfen a??a????daki <b>Hesab?? Do??rula</b> butonuna t??klay??n.</p><a href='https://localhost:5001{url}'><img src='https://n11scdn3.akamaized.net/a1/org/20/01/09/67/70/56/88/16/45/85/51/30/05202063912658290578.png' width=220 style='padding-top: 10px;'></a></td></tr></tbody></table>");
            
            CreateMessage("????lem Ba??ar??l?? !","Hesab??n??za onay maili g??nderilmi??tir !","success");
            return RedirectToAction("UserAccount");
        }

        public  async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                CreateMessage("Hata, Ge??ersiz Token !","B??yle bir token bulunamad??, hatan??n devam etmesi halinde site y??neticisi ile ileti??ime ge??iniz !","danger");
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                CreateMessage("Hata, Kullan??c?? Bulunamad?? !","B??yle bir kullan??c?? bulunamad??, hatan??n devam etmesi halinde site y??neticisi ile ileti??ime ge??iniz !","danger");
                return View();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    CreateMessage("Hata, E-mail Onaylanamad??",$"Email onaylanmad??, hatan??n devam etmesi halinde site y??neticisi ile ileti??ime ge??iniz ! Hata A????klamas??: '{item.Description}' ","danger");
                }                
                return View();
            }

            CreateMessage("????lem ba??ar??l??, Hesab??n??z Onayland?? !","Email hesab??n??z ba??ar??l?? bir ??ekilde onayland??. G??venli bir ??ekilde Al????veri??e devam edebilirsiniz !","success");
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