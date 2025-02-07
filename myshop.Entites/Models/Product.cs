using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace myshop.Entities.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string ProductName { get; set; }



        [DisplayName("Description")] 
        public string ProductDescription { get; set; }




        [DisplayName("Image")]
        [ValidateNever]
        public string ProductImg { get; set; }



        [Required]
        [DisplayName("Price")]
        public decimal ProductPrice { get; set; }



        [Required]
        [DisplayName("Category")]
     
        public int CategoryID { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }



    }
}
