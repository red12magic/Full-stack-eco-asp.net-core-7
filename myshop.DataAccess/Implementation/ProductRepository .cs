using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myshop.Entities.Models;
using myshop.Entities.Repositories;

namespace myshop.DataAccess.Implementation
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var ProductInDb = _context.Products.FirstOrDefault(x => x.ProductID == product.ProductID);
            if (ProductInDb != null)
            {
                ProductInDb.ProductName = product.ProductName;
                ProductInDb.ProductDescription = product.ProductDescription;
                ProductInDb.ProductPrice = product.ProductPrice;
                ProductInDb.ProductImg = product.ProductImg;
                ProductInDb.CategoryID = product.CategoryID;

               
                   

            }


        }
    }
}
