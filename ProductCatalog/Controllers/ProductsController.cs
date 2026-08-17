using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    public class ProductsController : Controller
    {
        // Instantiated directly — a legacy pattern vs. constructor injection
        private readonly ProductRepository _repository = new ProductRepository();

        // GET: Products
        public ActionResult Index(string search, int? categoryId, int page = 1)
        {
            int pageSize = int.Parse(ConfigurationManager.AppSettings["MaxPageSize"] ?? "20");

            var products = _repository.SearchProducts(search, categoryId);
            var categories = _repository.GetAllCategories();

            var viewModel = new ProductListViewModel
            {
                Products    = products.Skip((page - 1) * pageSize).Take(pageSize),
                SearchTerm  = search,
                CategoryFilter = categoryId,
                TotalCount  = products.Count(),
                Page        = page,
                PageSize    = pageSize,
                Categories  = categories.Select(c => new SelectListItem
                {
                    Value    = c.CategoryId.ToString(),
                    Text     = c.Name,
                    Selected = c.CategoryId == categoryId
                })
            };

            return View(viewModel);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var viewModel = new ProductViewModel
            {
                Product    = new Product(),
                Categories = _repository.GetAllCategories().Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text  = c.Name
                })
            };

            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.AddProduct(viewModel.Product);
                    TempData["SuccessMessage"] = "Product created successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes: " + ex.Message);
                }
            }

            // Re-populate categories on validation failure
            viewModel.Categories = _repository.GetAllCategories().Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text  = c.Name
            });

            return View(viewModel);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
                return HttpNotFound();

            var viewModel = new ProductViewModel
            {
                Product    = product,
                Categories = _repository.GetAllCategories().Select(c => new SelectListItem
                {
                    Value    = c.CategoryId.ToString(),
                    Text     = c.Name,
                    Selected = c.CategoryId == product.CategoryId
                })
            };

            return View(viewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateProduct(viewModel.Product);
                    TempData["SuccessMessage"] = "Product updated successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes: " + ex.Message);
                }
            }

            viewModel.Categories = _repository.GetAllCategories().Select(c => new SelectListItem
            {
                Value    = c.CategoryId.ToString(),
                Text     = c.Name,
                Selected = c.CategoryId == viewModel.Product.CategoryId
            });

            return View(viewModel);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _repository.DeleteProduct(id);
            TempData["SuccessMessage"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _repository.Dispose();

            base.Dispose(disposing);
        }
    }
}
