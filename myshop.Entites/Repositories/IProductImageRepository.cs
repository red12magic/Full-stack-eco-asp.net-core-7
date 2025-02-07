using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myshop.Entities.Models;

namespace myshop.Entities.Repositories
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {

        void Update(ProductImage productimage);

    }
}
