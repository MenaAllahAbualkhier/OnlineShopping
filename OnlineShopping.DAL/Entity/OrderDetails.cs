using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DAL.Entity
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int ItemId { get; set; }

        public double TotalPrice { get; set; }

        public int? Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
