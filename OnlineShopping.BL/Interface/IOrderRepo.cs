using OnlineShopping.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Interface
{
    public interface IOrderRepo:IBaseRepo<Order>
    {
        Order AddWithReturn(Order item);
    }
}
