using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;
using myshop.Utilities;

namespace myshop.Web.Areas.Customer.Controllers
{

    [Area ("Customer")]







    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public HomeController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

       private HomeViewModel LoadProductsAndCategories()
        {
            var products = _unitofwork.Product.GetAll();
            var category = _unitofwork.Category.GetAll();
            var img = _unitofwork.ProductImage.GetAll(Includwoed:"Product");
           

            return new HomeViewModel
            {
                Products = (List<Product>)products,
                Categories = (List<Category>)category,
                Images =   (List<ProductImage>)img,
            };


        }
        








        public IActionResult Index()
        {
            var ViewModel = LoadProductsAndCategories();
            //var products = _unitofwork.Product.GetAll(Includwoed:"Category");
            return View(ViewModel);
        }



        public IActionResult Details(int ProductId)

        {
            
            ShoppingCart obj = new ShoppingCart()
            {
                ProductId = ProductId,
                Product = _unitofwork.Product.GitFirstorDefult(v => v.ProductID == ProductId, Includwoed: "Category"),
                Count = 1
            };
            return View(obj);
   

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplictionUserId = claim.Value;

            ShoppingCart Cartobj = _unitofwork.ShoppingCart.GitFirstorDefult(
                u => u.ApplictionUserId == claim.Value && u.ProductId == shoppingCart.ProductId
                );


            if ( Cartobj == null)
            {
                _unitofwork.ShoppingCart.Add(shoppingCart);
                _unitofwork.Complete();

                HttpContext.Session.SetInt32(SD.SessionKey,

                    _unitofwork.ShoppingCart.GetAll(x => x.ApplictionUserId == claim.Value).ToList().Count()


                    );
               
            }
            else
            {
                _unitofwork.ShoppingCart.IncreaseCount(Cartobj, shoppingCart.Count);
                _unitofwork.Complete();
            }
           
      

            return RedirectToAction("Index");


        }




    }
}
