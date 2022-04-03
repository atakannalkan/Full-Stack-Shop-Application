using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace shopapp.webui.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "First Name field cannot be empty !")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name field cannot be empty !")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User Name field cannot be empty !")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Email field cannot be empty !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date Time field cannot be empty !")]
        public string DateTime { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CopyPassword { get; set; }

        public IList<string> SelectedRoles { get; set; }
        
    }
}