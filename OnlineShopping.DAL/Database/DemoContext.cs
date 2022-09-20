using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.DAL.Entity;
using OnlineShopping.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DAL.Database
{
    public class DemoContext:IdentityDbContext<ApplicationUser,IdentityRole,string>
    {
        public DemoContext(DbContextOptions<DemoContext> opt) : base(opt) { }
        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Item { get; set; }

        public DbSet<SubCategory> SubCategory { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }



    }
}
