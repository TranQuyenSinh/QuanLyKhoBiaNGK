using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoBiaNGK.Data;

namespace QuanLyKhoBiaNGK.Components;

public class DetailReceivedSelect : ViewComponent
{
    private readonly ApplicationDbContext _context;
    public DetailReceivedSelect(ApplicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var product = _context.Products.ToList();
        ViewData["Products"] = new SelectList(product, "Id", "Name");
        return View();
    }
}