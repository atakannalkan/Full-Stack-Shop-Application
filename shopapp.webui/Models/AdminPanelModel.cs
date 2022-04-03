using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using shopapp.entity;
using shopapp.webui.Identity;

namespace shopapp.webui.Models
{
    public class AdminPanelModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public IQueryable<User> Users { get; set; }
        public IQueryable<IdentityRole> Roles { get; set; }
    }
}