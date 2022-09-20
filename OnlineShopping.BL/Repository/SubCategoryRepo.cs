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
    public class SubCategoryRepo:BaseRepo<SubCategory>,ISubCategoryRepo
    {
        private readonly DemoContext demo;

        public SubCategoryRepo(DemoContext demo) : base(demo)
        {
            this.demo = demo;
        }

     
    }
}
