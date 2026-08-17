using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProductCatalog.Models;

namespace ProductCatalog.Data
{
    /// <summary>
    /// Repository pattern over Entity Framework 6 / ADO.NET.
    /// Uses synchronous calls — a common legacy pattern that Transform
    /// will modernize to async/await equivalents.
    /// </summary>
    public class ProductRepository : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository()
        {
            _context = new ApplicationDbContext();
        }

        // ── Products ──────────────────────────────────────────────────────────

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products
                           .Include(p => p.Category)
                           .Where(p => p.IsActive)
                           .OrderBy(p => p.Name)
                           .ToList();
        }

        public IEnumerable<Product> SearchProducts(string searchTerm, int? categoryId)
        {
            var query = _context.Products
                                .Include(p => p.Category)
                                .Where(p => p.IsActive);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) ||
                                         p.Description.Contains(searchTerm));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            return query.OrderBy(p => p.Name).ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                           .Include(p => p.Category)
                           .FirstOrDefault(p => p.ProductId == id);
        }

        public void AddProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.IsActive = true;
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            product.ModifiedDate = DateTime.Now;
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                // Soft delete
                product.IsActive = false;
                product.ModifiedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        // ── Categories ────────────────────────────────────────────────────────

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }

        // ── IDisposable ───────────────────────────────────────────────────────

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
