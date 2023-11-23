using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoBiaNGK.Models;
using System.Diagnostics;

namespace QuanLyKhoBiaNGK.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly SignInManager<User> _signInManager;

		public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager)
		{
			_logger = logger;
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			//return View("Dashboard");
			return RedirectToAction("Index", "ReceivedBills");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}