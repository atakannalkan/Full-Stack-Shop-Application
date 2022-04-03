using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "'Şifre' alanı boş bırakılamaz !")]
        [StringLength(20,MinimumLength = 5, ErrorMessage = "Şifre 5 ile 20 karakter arasında olmalıdır !")]
        public string Password { get; set; }

        [Required(ErrorMessage = "'Şifre Tekrar' alanı boş bırakılamaz !")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor !")]
        public string RePassword { get; set; }

        public string UserId { get; set; }
        public string Token { get; set; }
    }
}