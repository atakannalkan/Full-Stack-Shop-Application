using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class RoleModel
    {
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Name field cannot be empty !")]
        public string Name { get; set; }
    }
}