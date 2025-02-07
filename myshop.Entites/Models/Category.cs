using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace myshop.Entities.Models
{
    public class Category
    {

        public int CategoryID { get; set; }


        [Required] 
        public string CategotyName { get; set; }

        public string CategoryDescription { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string img {  get; set; }   //Add new veled t to categry can inseart img
        public  DateTime CreatedTimeCategory { get; set; } = DateTime.Now;//11/22/1024
    }
}
