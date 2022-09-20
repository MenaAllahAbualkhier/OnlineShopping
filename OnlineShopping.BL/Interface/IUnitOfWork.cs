using OnlineShopping.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepo Item { get; }
        ICategoryRepo Category { get; }

        ISubCategoryRepo SubCategory { get; }

        IOrderRepo Order { get; }

        IBaseRepo<OrderDetails> OrderDetails { get; }
        int Complete();
    }
}
