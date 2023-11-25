using CourseWorkWeb.Data;
using CourseWorkWeb.Models;
using CourseWorkWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseWorkWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        // private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> ProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(ProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVm = new()
            {
                CategoryList = _unitOfWork.Category
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVm);
            }
            else
            {
                //update
                productVm.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVm);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file!=null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var OldImagePath = 
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }

                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            return View();

        }

        // public IActionResult Edit(int? id)
        // {
        //     if (id == null || id == 0)
        //     {
        //         return NotFound();
        //     }
        //
        //     Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //     // Product? productFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        //     // Product? productFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
        //
        //     if (productFromDb == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(productFromDb);
        // }



        // [HttpPost]
        // public IActionResult Edit(Product product)
        // {
        //     // if (product.Name == product.DisplayOrder.ToString())
        //     // {
        //     //     ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
        //     // }
        //
        //     if (ModelState.IsValid)
        //     {
        //         _unitOfWork.Product.Update(product);
        //         _unitOfWork.Save();
        //         TempData["success"] = "Product updated successfully";
        //         return RedirectToAction("Index");
        //     }
        //
        //     return View();
        //
        // }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            // Product? productFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            // Product? productFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
