using Microsoft.AspNetCore.Mvc;

namespace CUSTOMERWEBSITE.Areas
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
