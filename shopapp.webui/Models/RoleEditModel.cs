using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class RoleEditModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }        
        public string[] AddToRole { get; set; }
        public string[] DeleteToRole { get; set; }
    }
}