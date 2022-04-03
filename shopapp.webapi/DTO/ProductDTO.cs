using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webapi.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } 
        public string ImageUrl { get; set; } 
    }
}