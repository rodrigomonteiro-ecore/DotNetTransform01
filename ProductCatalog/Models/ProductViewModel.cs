using System.Collections.Generic;
using System.Web.Mvc;

namespace ProductCatalog.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }

    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string SearchTerm { get; set; }
        public int? CategoryFilter { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
