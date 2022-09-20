using AutoMapper;
using OnlineShopping.BL.Model;
using OnlineShopping.DAL.Entity;
using OnlineShopping.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<ApplicationUser, RegistrationVM>();
            CreateMap< RegistrationVM, ApplicationUser>();

            CreateMap<Item, ItemVM>();
            CreateMap<ItemVM, Item>();

            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryVM, Category>();

            CreateMap<SubCategory, SubCategoryVM>();
            CreateMap<SubCategoryVM, SubCategory>();

            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();


            CreateMap<OrderDetails, OrderDetailsVM>();
            CreateMap<OrderDetailsVM, OrderDetails>();
        }
    }
}
