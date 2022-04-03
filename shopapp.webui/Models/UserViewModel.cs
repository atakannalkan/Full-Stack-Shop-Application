using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.webui.Identity;

namespace shopapp.webui.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}