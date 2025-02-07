using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Repositories
{
    public interface IGenericRepository<T> where T : class
    {





        //_Context.Categories.ToList();
        //_Context.Categories.Include("Products").ToList();
        //_Context.Categories.Where(x=>x.id ==id).ToList();
        IEnumerable<T> GetAll(Expression<Func <T, bool>>? perdictat = null, string? Includwoed = null);


        //_Context.Categories.Include("Products").ToSingleOrDefault();
        //_Context.Categories.Where(x=>x.id ==id).ToSingleOrDefault();
        T GitFirstorDefult(Expression<Func<T, bool>>? perdictat =null, string? Includwoed = null);

        //_Context.Categories.Add(Category)
        void Add(T entity);

        //_Context.Categories.Remove(Category)
        void Romove (T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
