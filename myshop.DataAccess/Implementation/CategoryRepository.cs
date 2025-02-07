using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myshop.Entities.Models;
using myshop.Entities.Repositories;

namespace myshop.DataAccess.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var CategoryInDb = _context.Categories.FirstOrDefault(x => x.CategoryID == category.CategoryID);
            if (CategoryInDb != null)
            {
                CategoryInDb.CategotyName = category.CategotyName;
                CategoryInDb.CategoryDescription = category.CategoryDescription;
                CategoryInDb.CreatedTimeCategory = DateTime.Now;
                CategoryInDb.img = category.img; // new img to change old img
            }
        }
    }
}

