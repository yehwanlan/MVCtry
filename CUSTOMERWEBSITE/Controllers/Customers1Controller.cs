using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUSTOMERWEBSITE.Models;

namespace CUSTOMERWEBSITE.Controllers
{
    [Route("/Customers1/{action=Index}/{CustomersID?}")]
    public class Customers1Controller : Controller
    {
        private readonly NorthwindContext _context;

        public Customers1Controller(NorthwindContext context)
        {
            _context = context;
        }
        //learn
        public IActionResult Index1()
        {
            return View();
        }
        [HttpGet]
      
        // GET: Customers1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers1/Details/5
        public async Task<IActionResult> Details(string CustomersID)
        {
            if (CustomersID == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == CustomersID);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers1/Edit/5
        public async Task<IActionResult> Edit(string CustomersID)
        {
            if (CustomersID == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(CustomersID);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string CustomersID, [Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (CustomersID != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers1/Delete/5
        public async Task<IActionResult> Delete(string CustomersID)
        {
            if (CustomersID == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == CustomersID);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string CustomersID)
        {
            var customer = await _context.Customers.FindAsync(CustomersID);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string CustomerID)
        {
            return _context.Customers.Any(e => e.CustomerId == CustomerID);
        }
    }
}
