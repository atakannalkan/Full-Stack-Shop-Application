using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "'Adı' alanı boş bırakılamaz !")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "'Adı' alanı 3 ile 20 karakter arasında olmalıdır !")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "'Soyadı' alanı boş bırakılamaz !")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "'Soyadı' alanı 3 ile 20 karakter arasında olmalıdır !")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "'Kullanıcı Adı' alanı boş bırakılamaz !")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "'Kullanıcı Adı' alanı 3 ile 15 karakter arasında olmalıdır !")]
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "'Email' alanı boş bırakılamaz !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "'Şifre' alanı boş bırakılamaz !")]
        [StringLength(20,MinimumLength = 5, ErrorMessage = "Şifre 5 ile 20 karakter arasında olmalıdır !")]
        public string Password { get; set; }

        [Required(ErrorMessage = "'Şifre Tekrar' alanı boş bırakılamaz !")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor !")]
        public string RePassword { get; set; }
    }
}