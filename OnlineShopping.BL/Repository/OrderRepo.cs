using OnlineShopping.BL.Interface;
using OnlineShopping.DAL.Database;
using OnlineShopping.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Repository
{
    internal class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        private readonly DemoContext demo;

        public OrderRepo(DemoContext demo) : base(demo)
        {
            this.demo = demo;
        }

        public Order AddWithReturn(Order item)
        {
            demo.Order.Add(item);
            demo.SaveChanges();
            return demo.Order.OrderBy(a => a.Id).LastOrDefault();
        }

    }
}
