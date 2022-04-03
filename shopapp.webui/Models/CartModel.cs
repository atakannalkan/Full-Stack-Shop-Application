using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class CartModel
    {
        public List<Cart> CartItem { get; set; }
        // public double ShoppingPrice { get {if(TotalPrice() > 3000) {return 0;} else {return 12.99;}} set{} }
        public double ShoppingPrice { get {return 12.99;} set{} }
        public double TotalPrice()
        {
            return CartItem.Sum(i => i.Product.Price * i.Quantity);
        }
        public int ProductCount()        
        {
            return CartItem.Count();
        }
        // public double AllTotalPrice
        // {
        //     get
        //     {
        //         if(TotalPrice() > 3000) {return TotalPrice();} else {
        //             return TotalPrice()+ShoppingPrice;
        //         }
        //     }
        //     set{} 
        // }
    }
}