using FormsApp.Models;
using FormsApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Win32.SafeHandles;


namespace FormsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageStorage _imageStore;
        
        public HomeController(IImageStorage imageStore)
        {
            _imageStore = imageStore;
        }
       
        public IActionResult Index(string? searchString, string? category)
        {
            var products = Repository.Products;
            var categories = Repository.Categories;
            searchString = searchString?.Trim();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                ViewBag.SearchString = searchString;
                products = products.Where(p => p.Name is not null && p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                if(!int.TryParse(category,  out var categoryId) || categoryId < 0)
                {
                    return BadRequest("Geçersiz kategori seçimi.");
                }

                if( categoryId > 0)
                {
                    var categoryExists = categories.Any(c => c.CategoryId == categoryId);

                    if (!categoryExists)
                    {
                        return NotFound("Kategori bulunamadı.");
                    }

                    products = products.Where(p => p.CategoryId == categoryId).ToList();
                }
                category = categoryId.ToString();
            }
           
            //ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name",category);

            var model = new ProductViewModel
            {
                Products = products,
                Categories = Repository.Categories,
                SelectedCategory = category
            };
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model, IFormFile imageFile)
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var uploadResult = await _imageStore.SaveAsync(imageFile);
            if (!uploadResult.IsSuccess)
            {
                ModelState.AddModelError("imageFile", uploadResult.ErrorMessage ?? "Resim Yüklenemedi");
               

                return View(model);
            }

            Repository.CreateProduct(model);
            model.Image = uploadResult.FileName;
            model.ProductId = Repository.Products.Count;
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
            if (entity == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
        {
            if (id != model.ProductId)
            {
                return NotFound();
            }
            var product = Repository.Products.FirstOrDefault(p=> p.ProductId ==id)?.Image;

            if(product == null) return NotFound();

            model.Image = product;
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (imageFile != null)
            {
                var uploadResullt = await _imageStore.SaveAsync(imageFile);
                if (!uploadResullt.IsSuccess)
                {
                    ModelState.AddModelError("imageFile", uploadResullt.ErrorMessage ?? "Resim Yüklenemedi");
                    return View(model);
                }
                model.Image = uploadResullt.FileName;
            }
           
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View("DeleteConfirm", entity);
        }

        [HttpPost]
        public IActionResult Delete(int id, int ProductId)
        {
            if (id != ProductId)
            {
                return NotFound();
            }
            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (entity == null)
            {
                return NotFound();
            }
            Repository.DeleteProduct(entity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditProduct(List<Product> Products)
        {
            foreach( var product in Products)
            {
                Repository.EditIsActive(product);

            }
            return RedirectToAction("Index");   
        }
    }
}
