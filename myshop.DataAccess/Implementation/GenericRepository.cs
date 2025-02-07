using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myshop.Entities.Repositories;

namespace myshop.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }



        public void Add(T entity)
        {

            //Categories.Add(Category);
            _dbSet.Add(entity);


        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? perdictat = null, string? Includwoed = null)
        {
            IQueryable<T> query = _dbSet;
            if(perdictat != null)
            {
                query = query.Where(perdictat);
            }


            if(Includwoed != null)
            {
                //_context.products.Include("Category,Logos,Users")
                foreach (var item in Includwoed.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);

                }
            }

            return query.ToList();
        }

        public T GitFirstorDefult(Expression<Func<T, bool>>? perdictat = null, string? Includwoed = null)
        {
            IQueryable<T> query = _dbSet;
            if (perdictat != null)
            {
                query = query.Where(perdictat);
            }


            if (Includwoed != null)
            {
                //_context.products.Include("Category,Logos,Users")
                foreach (var item in Includwoed.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);

                }
            }

            return query.SingleOrDefault();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
          _dbSet.RemoveRange(entities);
        }

        public void Romove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
