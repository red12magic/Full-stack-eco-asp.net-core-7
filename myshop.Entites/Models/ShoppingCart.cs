using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace myshop.Entities.Models
{
    public class ShoppingCart
    {

        public int Id { get; set; } 

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }


        [Range(1, 100, ErrorMessage = "You must enter value between 1 to 100 ")]
        public int Count { get; set; }

        public string ApplictionUserId { get; set; }
        [ForeignKey("ApplictionUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
