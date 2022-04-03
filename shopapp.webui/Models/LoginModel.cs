using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "'Kullanıcı Adı' alanı boş bırakılamaz !")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "'Şifre' alanı boş bırakılamaz !")]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}