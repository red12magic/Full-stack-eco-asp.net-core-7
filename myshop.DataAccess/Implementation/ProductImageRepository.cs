using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myshop.Entities.Models;
using myshop.Entities.Repositories;

namespace myshop.DataAccess.Implementation
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ProductImage productimge)
        {
            var ProductImgeInDb = _context.ProductImages.FirstOrDefault(x => x.ProductImageID == productimge.ProductImageID);
            if (ProductImgeInDb != null)
            {
                ProductImgeInDb.ProductID = productimge.ProductID;

                ProductImgeInDb.ImagePathFirst = productimge.ImagePathFirst;
                ProductImgeInDb.ImagePathSeconed = productimge.ImagePathSeconed;
                ProductImgeInDb.ImagePathTheard = productimge.ImagePathTheard;
                ProductImgeInDb.ImagePathFord = productimge.ImagePathFord;


            }


        }
    }
}
