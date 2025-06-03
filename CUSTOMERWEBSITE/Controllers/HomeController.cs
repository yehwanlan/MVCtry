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
        private readonly NorthwindContext _dc; // 1. �[�o�@��

        public HomeController(ILogger<HomeController> logger, NorthwindContext dc)
        {
            _logger = logger;
            _dc = dc; // 2. �s�_��
        }

        public IActionResult Index()
        {
            ViewBag.Test = "����";
            ViewBag.ispan149 = "���ݯZ";
            ViewBag.Country = _dc.Customers.OrderBy(c => c.CustomerId).Last();
            ViewBag.Country1 = new SelectList(_dc.Customers.Select(c => c.Country).Distinct());
            ViewBag.CustomerCount = $"alert('�Ȥ�H��:{_dc.Customers.Count()}')";
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
            System.Diagnostics.Debug.WriteLine("�w�i�J Contact Controller Action�I");
            System.Diagnostics.Debug.WriteLine($"Name: {cvm.Name}, Email: {cvm.Email}, Phone: {cvm.Phone}");

            if (ModelState.IsValid)
            {
                // TODO: ��Ʈw�g�J contact
                return RedirectToAction("index");
            }
            return View(cvm);
        }

    }
}
