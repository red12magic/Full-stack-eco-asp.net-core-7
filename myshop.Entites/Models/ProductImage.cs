using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }

       
        public string ImagePathFirst { get; set; } // Path or URL to the image
        public string ImagePathSeconed { get; set; } // Path or URL to the image
        public string ImagePathTheard { get; set; } // Path or URL to the image
        public string ImagePathFord { get; set; } // Path or URL to the image
        [Required]
        public int ProductID { get; set; } // Foreign key

        public Product Product { get; set; } // Navigation property
    }

}
