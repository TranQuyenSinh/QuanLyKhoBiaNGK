using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoBiaNGK.Data;
using QuanLyKhoBiaNGK.Models;

namespace QuanLyKhoBiaNGK.Controllers
{
    public class ReceivedBillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceivedBillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReceivedBills
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReceivedBills.Include(r => r.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReceivedBills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceivedBills == null)
            {
                return NotFound();
            }

            var receivedBill = await _context.ReceivedBills
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receivedBill == null)
            {
                return NotFound();
            }

            return View(receivedBill);
        }

        // GET: ReceivedBills/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "FullName");
            return View();
        }

        // POST: ReceivedBills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Note,SupplierId,Total")] ReceivedBill receivedBill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receivedBill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "FullName", receivedBill.SupplierId);
            return View(receivedBill);
        }

        [HttpPost]
        public async Task<IActionResult> GetDetailReceivedPartial(
            [FromForm]
        string unit,
        int price,
        int quantity,
        int amount,
        int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var model = new DetailReceived()
            {
                Unit = unit,
                Price = price,
                Quantity = quantity,
                Amount = amount,
                Product = product
            };

            return PartialView("_DetailReceivedPartial", model);
        }

        // GET: ReceivedBills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceivedBills == null)
            {
                return NotFound();
            }

            var receivedBill = await _context.ReceivedBills.FindAsync(id);
            if (receivedBill == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "FullName", receivedBill.SupplierId);
            return View(receivedBill);
        }

        // POST: ReceivedBills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Note,SupplierId,Total")] ReceivedBill receivedBill)
        {
            if (id != receivedBill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receivedBill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceivedBillExists(receivedBill.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "FullName", receivedBill.SupplierId);
            return View(receivedBill);
        }

        // GET: ReceivedBills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceivedBills == null)
            {
                return NotFound();
            }

            var receivedBill = await _context.ReceivedBills
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receivedBill == null)
            {
                return NotFound();
            }

            return View(receivedBill);
        }

        // POST: ReceivedBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceivedBills == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReceivedBill'  is null.");
            }
            var receivedBill = await _context.ReceivedBills.FindAsync(id);
            if (receivedBill != null)
            {
                _context.ReceivedBills.Remove(receivedBill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceivedBillExists(int id)
        {
            return (_context.ReceivedBills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
