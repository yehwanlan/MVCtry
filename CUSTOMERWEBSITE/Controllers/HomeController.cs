using CUSTOMERWEBSITE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CUSTOMERWEBSITE.ViewModels;

namespace CUSTOMERWEBSITE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _dc; // 1. 加這一行

        public HomeController(ILogger<HomeController> logger, NorthwindContext dc)
        {
            _logger = logger;
            _dc = dc; // 2. 存起來
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Customers()
        {
            // 3. 用 _dc 來查資料
            var customers = _dc.Customers.ToList(); // 加 .ToList() 比較保險
            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: HOME/Contact
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModels cvm)
        {
            if (ModelState.IsValid)
            {
                // 導到 ContactResult 頁面，顯示剛填的資料
                return View("ContactResult", cvm);
            }
            // 失敗就回原表單頁面
            return View(cvm);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
