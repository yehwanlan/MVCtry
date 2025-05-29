using CUSTOMERWEBSITE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CUSTOMERWEBSITE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _dc; // 1. �[�o�@��

        public HomeController(ILogger<HomeController> logger, NorthwindContext dc)
        {
            _logger = logger;
            _dc = dc; // 2. �s�_��
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Customers()
        {
            // 3. �� _dc �Ӭd���
            var customers = _dc.Customers.ToList(); // �[ .ToList() ����O�I
            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
