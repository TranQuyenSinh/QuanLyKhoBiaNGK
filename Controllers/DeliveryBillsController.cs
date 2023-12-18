using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoBiaNGK.Data;
using QuanLyKhoBiaNGK.Data.Migrations;
using QuanLyKhoBiaNGK.Models;

namespace QuanLyKhoBiaNGK.Controllers
{
    public class DeliveryBillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryBillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryBills
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeliveryBills.Include(d => d.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeliveryBills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryBill == null)
            {
                return NotFound();
            }

            var deliveryBill = await _context.DeliveryBills
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryBill == null)
            {
                return NotFound();
            }

            var detailBill = await _context.DeliveryBillItems
                            .Include(x => x.Product)
                            .Where(x => x.DeliveryBillId == id).ToListAsync();

            if (detailBill == null)
            {
                return NotFound();
            }
            ViewData["DetailBill"] = detailBill;
            ViewBag.ReturnUrl = Request.GetDisplayUrl();
            return View(deliveryBill);
        }

        // GET: DeliveryBills/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName");
            return View();
        }

        // POST: DeliveryBills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Note,CustomerId")] DeliveryBill deliveryBill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryBill);
                await _context.SaveChangesAsync();
                return RedirectToAction("");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", deliveryBill.CustomerId);
            return View(deliveryBill);

        }
        public IActionResult CreateDetail(int billId)
        {
            var bill = _context.DeliveryBills.Find(billId);
            if (bill == null) return NotFound();

            var model = new DeliveryBillItem();
            ViewData["BillId"] = billId;
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
            return View(model);
        }

        // POST: ReceivedBills/CreateDetail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetail(int billId, DeliveryBillItem model)
        {
            var bill = await _context.DeliveryBills.FindAsync(billId);
            if (bill == null) return NotFound();

            var product = await _context.Products.FindAsync(model.ProductId);
            if (product == null) return NotFound();
            model.DeliveryBillId = billId;
            model.Amount = model.Price * model.Quantity;
            _context.Add(model);

            bill.Total += model.Amount;
            product.Inventory -= model.Quantity;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { Id = billId });
        }


        //delete detail
        public async Task<IActionResult> DeleteDetail(int? detailId, string returnUrl)
        {
            var detail = await _context.DeliveryBillItems
                .Include(m => m.DeliveryBill).Include(x => x.Product).FirstOrDefaultAsync(X => X.Id == detailId);


            if (detail == null)
            {
                return NotFound("ko thay detail");
            }
            detail.DeliveryBill.Total -= detail.Amount;
            detail.Product.Inventory += detail.Quantity;
            _context.Remove(detail);
            await _context.SaveChangesAsync();

            return Redirect(returnUrl);
        }


        // GET: DeliveryBills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeliveryBill == null)
            {
                return NotFound();
            }

            var deliveryBill = await _context.DeliveryBill.FindAsync(id);
            if (deliveryBill == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", deliveryBill.CustomerId);
            return View(deliveryBill);
        }

        // POST: DeliveryBills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeliveryBill deliveryBill)
        {
            if (id != deliveryBill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bill = _context.DeliveryBills.Find(id);
                    bill.Note = deliveryBill.Note;
                    bill.CustomerId = deliveryBill.CustomerId;
                    bill.Total = deliveryBill.Total;
                    bill.Date = deliveryBill.Date;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryBillExists(deliveryBill.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", deliveryBill.CustomerId);
            return View(deliveryBill);
        }

        // GET: DeliveryBills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryBill == null)
            {
                return NotFound();
            }

            var deliveryBill = await _context.DeliveryBill
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryBill == null)
            {
                return NotFound();
            }

            return View(deliveryBill);
        }

        // POST: DeliveryBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryBill == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DeliveryBill'  is null.");
            }
            var deliveryBill = await _context.DeliveryBill
                                .Include(x => x.DeliveryBillItem)
                                .ThenInclude(x => x.Product)
                                .FirstOrDefaultAsync(x => x.Id == id);
            if (deliveryBill != null)
            {
                deliveryBill.DeliveryBillItem.ToList().ForEach(detail => detail.Product.Inventory += detail.Quantity);
                _context.DeliveryBill.Remove(deliveryBill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryBillExists(int id)
        {
            return (_context.DeliveryBill?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
