using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myshop.Entities.Models;

namespace myshop.Entities.ViewModels
{
    public class HomeViewModel
    {

        public List<Product> Products { get; set; }

        public List<ProductImage> Images { get; set; }

        public List<Category> Categories { get; set; }
    }
}
