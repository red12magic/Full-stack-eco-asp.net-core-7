using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using myshop.DataAccess;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;
using Stripe;


namespace myshop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {

        private IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IUnitOfWork UnitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Index()
        {

            var categories = _UnitOfWork.Category.GetAll();
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]//scuratiy
        //public IActionResult Create(Category category)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        //  _context.Categories.Add(category);
        //        _UnitOfWork.Category.Add(category);
        //        // _context.SaveChanges();
        //        _UnitOfWork.Complete();
        //        TempData["Create"] = "Item Created Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]//scuratiy
        public IActionResult Create(Category category, IFormFile upload)
        {

            if (ModelState.IsValid)
            {

                string RootPath = _webHostEnvironment.WebRootPath;
                if (upload != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Category");
                    var ext = Path.GetExtension(upload.FileName);

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        upload.CopyTo(filestream);
                    }
                    category.img = @"Images\Category\" + filename + ext;

                }


                _UnitOfWork.Category.Add(category);
                _UnitOfWork.Complete();
                TempData["Create"] = "Item Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null | id == 0)
            {
                NotFound();
            }

            // var CategoryInDb = _context.Categories.Find(id);

            var categoryIndb = _UnitOfWork.Category.GitFirstorDefult(x => x.CategoryID == id);
            return View(categoryIndb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]//scuratiy
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //   _context.Categories.Update(category);

                _UnitOfWork.Category.Update(category);

                //_context.SaveChanges();

                _UnitOfWork.Complete();

                TempData["Update"] = "Item has Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);

        }




        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id == null | id == 0)
            {
                NotFound();
            }

            //var CategoryInDb = _context.Categories.Find(id);

            var categoryIndb = _UnitOfWork.Category.GitFirstorDefult(x => x.CategoryID == id);
            return View(categoryIndb);
        }



        [HttpPost]
        public IActionResult DeleteCategory(int? CategoryID)
        {

            //var CategoryInDb = _context.Categories.Find(CategoryId);
            var categoryIndb = _UnitOfWork.Category.GitFirstorDefult(x => x.CategoryID == CategoryID);

            if (categoryIndb == null)
            {
                NotFound();
            }


            //_context.Categories.Remove(CategoryInDb);
            _UnitOfWork.Category.Romove(categoryIndb);
            //_context.SaveChanges();
            _UnitOfWork.Complete();
            TempData["Delete"] = "Data Has Deleted Succesfully";

            return RedirectToAction("Index");
        }


    }
}



