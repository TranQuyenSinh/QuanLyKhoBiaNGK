using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoBiaNGK.Data;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhoBiaNGK.Controllers
{
    public class StatisticsController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public StatisticsController(ApplicationDbContext context) { 
            _context = context;
        }

        // GET: StatisticsController
        public ActionResult Inventory()
        {
            var products = _context.Products.Include(x=>x.Category).ToList();
            var exhaustProducts = _context.Products.Where(x => x.Inventory < x.InventoryLevel).Include(x => x.Category).ToList();
            ViewData["ExhaustProducts"] = exhaustProducts;
            return View(products);

        }
    }
}
