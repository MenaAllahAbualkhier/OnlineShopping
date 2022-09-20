using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Interface
{
    public interface IBaseRepo<T>where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T,bool>>?match=null,string? Property=null);
        T GetById(Expression<Func<T, bool>> match, string? Property=null);

        void Add(T item);
        void AddRange(List<T> items);

        void Delete(T item);

        void DeteteRange(IEnumerable<T> items);

        void Update(T item);
    }
}
