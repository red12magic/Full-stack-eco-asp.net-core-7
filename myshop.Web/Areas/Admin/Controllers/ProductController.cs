using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using myshop.DataAccess;
using myshop.DataAccess.Implementation;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;
using static System.Net.Mime.MediaTypeNames;


namespace myshop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork UnitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
           }



        public IActionResult Index()
        {

            
            return View();
        }

        public IActionResult GetData()
        {

            var products = _UnitOfWork.Product.GetAll(Includwoed:"Category");
            return Json(new {data = products});
        }



        [HttpGet]
        public IActionResult Create()
        {
   


            ProductVM productsVM = new ProductVM()
            {

               
                Product = new Product(),
                CategoryList = _UnitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.CategotyName,
                    Value = x.CategoryID.ToString()
                })
            };
            return View(productsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
          

            
                string rootPath = _webHostEnvironment.WebRootPath;

                // 1. حفظ الصورة الرئيسية في جدول Product
                if (productVM.Uploads != null && productVM.Uploads.Any())
                {
                    string mainImageFileName = Guid.NewGuid().ToString();
                    var uploadPath = Path.Combine(rootPath, @"Images\Products");
                    var extension = Path.GetExtension(productVM.Uploads[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploadPath, mainImageFileName + extension), FileMode.Create))
                    {
                        productVM.Uploads[0].CopyTo(fileStream);
                    }

                    productVM.Product.ProductImg = @"Images\Products\" + mainImageFileName + extension;
                }

                // إضافة المنتج الرئيسي
                _UnitOfWork.Product.Add(productVM.Product);
                _UnitOfWork.Complete();

                // 2. توزيع الصور على الحقول المختلفة في جدول ProductImage
                if (productVM.Uploads.Count > 1)
                {
                    var productImage = new ProductImage
                    {
                        ProductID = productVM.Product.ProductID
                    };

                    for (int i = 1; i < productVM.Uploads.Count; i++)
                    {
                        string imageFileName = Guid.NewGuid().ToString();
                        var uploadPath = Path.Combine(rootPath, @"Images\Products\");
                        var extension = Path.GetExtension(productVM.Uploads[i].FileName);

                        using (var fileStream = new FileStream(Path.Combine(uploadPath, imageFileName + extension), FileMode.Create))
                        {
                            productVM.Uploads[i].CopyTo(fileStream);
                        }

                        string imagePath = @"Images\Products\" + imageFileName + extension;

                        // توزيع الصور على الحقول المختلفة
                        if (i == 1) productImage.ImagePathFirst = imagePath;
                        else if (i == 2) productImage.ImagePathSeconed = imagePath;
                        else if (i == 3) productImage.ImagePathTheard = imagePath;
                        else if (i == 4) productImage.ImagePathFord = imagePath;
                    }

                    // إضافة السجل إلى جدول ProductImage
                    _UnitOfWork.ProductImage.Add(productImage);
                    _UnitOfWork.Complete();
                

                TempData["Create"] = "Item Created Successfully";
                return RedirectToAction("Index");
            }

            // إذا كان هناك خطأ في المدخلات
            return View(productVM);
        }













        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null | id == 0)
            {
                NotFound();
            }


            ProductVM productVM = new ProductVM()
            {
                Product = _UnitOfWork.Product.GitFirstorDefult(x => x.ProductID == id),
                CategoryList = _UnitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.CategotyName,
                    Value = x.CategoryID.ToString()
                })
            };


            return View(productVM);


        }




        [HttpPost]
        [ValidateAntiForgeryToken]//scuratiy
        public IActionResult Edit(ProductVM productVM , IFormFile? upload)
        {
            if (ModelState.IsValid)
            {

                string RootPath = _webHostEnvironment.WebRootPath;
                if (upload != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(upload.FileName);

                    if (productVM.Product.ProductImg != null)
                    {
                        var oldimg = Path.Combine(RootPath, productVM.Product.ProductImg.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimg))
                        {
                            System.IO.File.Delete(oldimg);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        upload.CopyTo(filestream);
                    }
                    productVM.Product.ProductImg = @"Images\Products\" + filename + ext;

                }
                //   _context.Categories.Update(product);

                _UnitOfWork.Product.Update(productVM.Product);

                //_context.SaveChanges();

                _UnitOfWork.Complete();

                TempData["Update"] = "Item has Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(productVM.Product);

        }






        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            //var ProductInDb = _context.Categories.Find(ProductId);
            var productIndb = _UnitOfWork.Product.GitFirstorDefult(x => x.ProductID == id);

            if (productIndb == null)
            {
                return Json(new {success = false , message = "Error While Deleting"});
            }


            //_context.Categories.Remove(ProductInDb);
            _UnitOfWork.Product.Romove(productIndb);


            var oldimg = Path.Combine(_webHostEnvironment.WebRootPath, productIndb.ProductImg.TrimStart('\\'));
            if (System.IO.File.Exists(oldimg))
            {
                System.IO.File.Delete(oldimg);
            }


            //_context.SaveChanges();
            _UnitOfWork.Complete();
            return Json(new { success = true, message = "File has been Deleting" });

             
        }


    }
}



