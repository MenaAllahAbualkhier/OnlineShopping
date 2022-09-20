using Microsoft.EntityFrameworkCore;
using OnlineShopping.BL.Interface;
using OnlineShopping.DAL.Database;
using OnlineShopping.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly DemoContext demo;

        public BaseRepo(DemoContext demo)
        {
            this.demo = demo;
        }
        public void Add(T item)
        {
            demo.Set<T>().Add(item);
        }


        public void AddRange(List<T> items)
        {
            demo.Set<T>().AddRange(items);
            demo.SaveChanges(); 
        }

        public void Delete(T item)
        {
            demo.Set<T>().Remove(item);
        }

        public void DeteteRange(IEnumerable<T> items)
        {
            demo.Set<T>().RemoveRange(items);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? match = null, string? Property = null)
        {
            IQueryable<T>query=demo.Set<T>();
            if(match != null)
            {
                query = query.Where(match);
            }
            if(Property != null)
            {
                foreach(var item in Property.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public T GetById(Expression<Func<T, bool>>match , string? Property = null)
        {
            IQueryable<T> query=demo.Set<T>();
            query = query.Where(match);
            if (Property != null)
            {
                foreach(var item in Property.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
                 
            }
            return query.FirstOrDefault();
        }

        public void Update(T item)
        {
            demo.Entry(item).State = EntityState.Modified;
        }


    }
}
