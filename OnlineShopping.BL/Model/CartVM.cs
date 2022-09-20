using OnlineShopping.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Model
{
    public class CartVM
    {
        public Item Item { get; set; } = new Item();
        public int? Quantity { get; set; }

    }
}
