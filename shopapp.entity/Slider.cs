using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.entity
{
    public class Slider
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
    }
}