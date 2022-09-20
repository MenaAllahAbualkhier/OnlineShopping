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
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        private readonly DemoContext demo;

        public CategoryRepo(DemoContext demo) : base(demo)
        {
            this.demo = demo;
        }

        public void Edit(Category model)
        {
            var data=demo.Category.Find(model.Id);
            data.Name=model.Name;

        }
    }
}
