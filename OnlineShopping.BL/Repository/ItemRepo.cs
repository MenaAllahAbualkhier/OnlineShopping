using OnlineShopping.BL.Interface;
using OnlineShopping.DAL.Database;
using OnlineShopping.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Repository
{
    public class ItemRepo: BaseRepo<Item>, IItemRepo
    {
        private readonly DemoContext demo;

        public ItemRepo(DemoContext demo) : base(demo)
        {
            this.demo = demo;
        }

     
    }
}
