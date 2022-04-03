using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
        public List<Slider> Sliders { get; set; }
    }

    public class PageInfo
    {
        public int TotalItems { get; set; } // Veritabanındaki ürün sayısı
        public int PageSize { get; set; } // Sayfa başına kaç tane ürün göstereceğiz
        public int CurrentPage { get; set; } // Kaçıncı sayfadayız, 1'mi 2'mi 3'mü ?
        public string CurrentCategory { get; set; } // Kategoriye göremi listeleyeceğiz yoksa bütün ürünlere göremi

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems/PageSize);
            // 10/3 = 3.3 =>
        }
    }
}