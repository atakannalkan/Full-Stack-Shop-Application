using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name field cannot be empty !")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Number must be between 5 and 30")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Url field cannot be empty !")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Number must be between 5 and 30")]
        public string Url { get; set; }

        public List<Product> Products { get; set; }
    }
}