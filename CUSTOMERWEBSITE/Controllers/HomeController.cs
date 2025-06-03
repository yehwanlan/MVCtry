using CUSTOMERWEBSITE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CUSTOMERWEBSITE.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.Test = "測試";
            ViewBag.ispan149 = "全端班";
            ViewBag.Country = _dc.Customers.OrderBy(c => c.CustomerId).Last();
            ViewBag.Country1 = new SelectList(_dc.Customers.Select(c => c.Country).Distinct());
            ViewBag.CustomerCount = $"alert('客戶人數:{_dc.Customers.Count()}')";
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModels cvm)
        {
            System.Diagnostics.Debug.WriteLine("已進入 Contact Controller Action！");
            System.Diagnostics.Debug.WriteLine($"Name: {cvm.Name}, Email: {cvm.Email}, Phone: {cvm.Phone}");

            if (ModelState.IsValid)
            {
                // TODO: 資料庫寫入 contact
                return RedirectToAction("index");
            }
            return View(cvm);
        }

    }
}
