using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoBiaNGK.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace QuanLyKhoBiaNGK.Controllers
{
    [Authorize]
    //ThongKe
    public class StatisticsController : Controller
    {

        private readonly ApplicationDbContext _context;
        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatisticsController
        public ActionResult Inventory()
        {
            var products = _context.Products.Include(x => x.Category).ToList();
            var exhaustProducts = _context.Products.Where(x => x.Inventory < x.InventoryLevel).Include(x => x.Category).ToList();
            ViewData["ExhaustProducts"] = exhaustProducts;
            return View(products);
        }

        public IActionResult Profit(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate.HasValue && toDate.HasValue)
            {
                ViewBag.FromDate = fromDate.Value;
                ViewBag.ToDate = toDate.Value;
                return View();
            }
            return View();
        }

        // Report nhập hàng
        public IActionResult GetReportImport(DateTime fromDate, DateTime toDate)
        {
            var products = _context.Products.ToList();

            var data = new List<object>();

            var totalAmount = 0;
            foreach (var product in products)
            {
                var total = _context.DetailReceiveds.Include(x => x.ReceivedBill)
                                    .Where(x => x.ProductId == product.Id)
                                    .Where(x => x.ReceivedBill.Date >= fromDate && x.ReceivedBill.Date <= toDate)
                                    .Sum(x => x.Quantity);

                var amount = product.PurchasePrice * total;
                data.Add(new
                {
                    ProductName = product.Name,
                    Unit = product.Unit,
                    PurchasePrice = product.PurchasePrice,
                    CurrentInventory = product.Inventory,
                    TotalQuantity = total,
                    Amount = amount
                });
                totalAmount += amount;
            }

            return new JsonResult(new
            {
                data,
                totalAmount
            });
        }

        // Doanh thu theo ngày
        public IActionResult GetProfitByDay(DateTime fromDate, DateTime toDate)
        {
            var d = fromDate;
            var data = new List<object>();
            while (d < toDate)
            {
                var totalProfitInDay = _context.DeliveryBills
                .ToList()
                .Where(x => x.Date.ToString("dd/MM/yyyy") == d.ToString("dd/MM/yyyy"))
                .Sum(bill => bill.SubTotal);

                data.Add(new
                {
                    Date = d.ToString("dd/MM/yyyy"),
                    ProfitInDay = totalProfitInDay
                });

                d = d.AddDays(1);
            }
            return new JsonResult(data);
        }

        // Report bán hàng
        public IActionResult GetReportQuantity(DateTime fromDate, DateTime toDate)
        {
            var products = _context.Products.ToList();

            var data = new List<object>();
            var totalProfit = 0;
            foreach (var product in products)
            {
                var total = _context.DeliveryBillItems.Include(x => x.DeliveryBill)
                                    .Where(x => x.ProductId == product.Id)
                                    .Where(x => x.DeliveryBill.Date >= fromDate && x.DeliveryBill.Date <= toDate)
                                    .Sum(x => x.Quantity);
                var von = product.PurchasePrice * total;
                var doanhthu = product.RetailPrice * total;
                var profit = doanhthu - von;
                data.Add(new
                {
                    ProductName = product.Name,
                    Unit = product.Unit,
                    PurchasePrice = product.PurchasePrice,
                    RetailPrice = product.RetailPrice,
                    TotalQuantity = total,
                    Von = von,
                    DoanhThu = doanhthu,
                    Profit = profit
                });
                totalProfit += profit;
            }

            return new JsonResult(new
            {
                totalProfit,
                data
            });
        }

        public IActionResult GetReportCustomerBillCount(DateTime fromDate, DateTime toDate)
        {
            var customers = _context.Customers.Include(x => x.Bills).ToList();

            var data = new List<object>();
            customers.ForEach(customer =>
            {
                var bills = customer.Bills.Where(x => x.Date >= fromDate && x.Date <= toDate).ToList();
                data.Add(new
                {
                    CustomerName = customer.FullName,
                    billCount = bills.Count,
                    totalPrice = bills.Sum(b => b.Total)
                });
            }
            );

            return new JsonResult(data);
        }
    }
}
