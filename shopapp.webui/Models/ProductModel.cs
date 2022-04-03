using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Name field cannot be empty !!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Number must be between 5 and 30")]
        public string Name { get; set; }
        
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Number must be between 5 and 30")]
        [Required(ErrorMessage = "Url field cannot be empty !")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Price field cannot be empty !")]
        [Range(0,100000,ErrorMessage = "Number must be between 0 and 100000")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Rating field cannot be empty !")]
        // [Range(0,50, ErrorMessage = "Number must be between 0 and 5")]
        public string Rating { get; set; }
        
        [Required(ErrorMessage = "Stock field cannot be empty !")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Description field cannot be empty !")]
        [StringLength(400, ErrorMessage = "400 character limit exceeded !")]
        public string Description { get; set; }        

        // [Required(ErrorMessage = "Image Url field cannot be empty !")]
        public string ImageUrl { get; set; }
        public bool IsHome { get; set; }
        public bool IsApproved { get; set; }
        public string DateAdded { get; set; }

        public List<Category> SelectedCategories { get; set; }
    }
}