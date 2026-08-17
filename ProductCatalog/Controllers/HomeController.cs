using System.Web.Mvc;

namespace ProductCatalog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A legacy Product Catalog built on ASP.NET MVC 5 / .NET Framework 4.8.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch with Contoso Corp.";
            return View();
        }
    }
}
