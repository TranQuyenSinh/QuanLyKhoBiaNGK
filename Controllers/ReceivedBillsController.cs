using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
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

            var detailBill = await _context.DetailReceiveds
                            .Include(x => x.Product)
                            .Where(x => x.BillId == id).ToListAsync();

            ViewData["DetailBill"] = detailBill;
            ViewBag.returnUrl = Request.GetDisplayUrl();


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
        public async Task<IActionResult> Create([Bind("Date,Note,SupplierId")] ReceivedBill receivedBill)
        {
            _context.Add(receivedBill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = receivedBill.Id });
        }

        // GET: ReceivedBills/CreateDetail
        public IActionResult CreateDetail(int billId)
        {
            var bill = _context.ReceivedBills.Find(billId);
            if (bill == null) return NotFound();


            var model = new DetailReceived();
            ViewData["BillId"] = billId;
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
            return View(model);
        }

        // POST: ReceivedBills/CreateDetail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetail(int billId, DetailReceived model)
        {
            var bill = await _context.ReceivedBills.FindAsync(billId);
            if (bill == null) return NotFound();

            model.BillId = billId;
            model.Amount = model.Price * model.Quantity;
            _context.Add(model);

            bill.Total += model.Amount;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { Id = billId });
        }

        public async Task<IActionResult> DeleteDetail(int? detailId, string returnUrl)
        {
            var detail = await _context.DetailReceiveds
                                .Where(x => x.Id == detailId)
                                .Include(x => x.ReceivedBill)
                                .FirstOrDefaultAsync();
            if (detail == null)
            {
                return NotFound();
            }

            detail.ReceivedBill.Total -= detail.Amount;

            _context.Remove(detail);
            await _context.SaveChangesAsync();

            return Redirect(returnUrl);
        }





        // GET: ReceivedBills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceivedBills == null)
            {
                return NotFound();
            }

            var receivedBill = await _context.ReceivedBills
                                .Where(x => x.Id == id)
                                .Include(x => x.Supplier).FirstOrDefaultAsync();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Note,SupplierId")] ReceivedBill receivedBill)
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
