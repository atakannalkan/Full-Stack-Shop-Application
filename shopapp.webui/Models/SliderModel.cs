using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class SliderModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Image Url field cannot be empty !")]
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        
        public bool Active { get; set; }
    }
}