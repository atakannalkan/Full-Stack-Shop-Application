using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class UserCreateModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "'First Name field cannot be empty !")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name field cannot be empty !")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName field cannot be empty !")]
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Email field cannot be empty !")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        [Required(ErrorMessage = "Password field cannot be empty !")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re Password field cannot be empty !")]
        [Compare("Password", ErrorMessage = "Passwords do not match !")]
        public string RePassword { get; set; }
    }
}