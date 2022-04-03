using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using shopapp.webui.Identity;

namespace shopapp.webui.Models
{
    public class RoleDetailsModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}