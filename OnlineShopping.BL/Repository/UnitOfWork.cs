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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemoContext demo;
        public IItemRepo Item { get; private set; }
        public ICategoryRepo Category { get; private set; }
        public ISubCategoryRepo SubCategory { get; }

        public IOrderRepo Order { get; }

        public IBaseRepo<OrderDetails> OrderDetails { get; }


        public UnitOfWork(DemoContext demo)
        {
            this.demo = demo;
            Item =new  ItemRepo(demo);
            Category = new CategoryRepo(demo);
            SubCategory = new SubCategoryRepo(demo);
            Order=new OrderRepo(demo);    
            OrderDetails=new BaseRepo<OrderDetails>(demo);  
        }

        public int Complete()
        {
           return demo.SaveChanges();
        }

        public void Dispose()
        {
            demo.Dispose();
        }
    }
}
