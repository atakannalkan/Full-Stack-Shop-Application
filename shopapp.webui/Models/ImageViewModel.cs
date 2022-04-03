using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class ImageViewModel
    {
        public IEnumerable<string> IndexImageUrl { get; set; }
        public IEnumerable<string> SliderImageUrl { get; set; }
        public IEnumerable<string> SvgImageUrl { get; set; }
        public IEnumerable<string> IconImageUrl { get; set; }
    }
}