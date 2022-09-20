using OnlineShopping.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DAL.Entity
{
    public class Order
    {
        public string ApplicationUserId { get; set; } = "";
        public ApplicationUser ApplicationUser { get; set; }
        public int Id { get; set; }
    }
}
