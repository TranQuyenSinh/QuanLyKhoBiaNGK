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
		private readonly SignInManager<IdentityUser> _signInManager;

		public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager)
		{
			_logger = logger;
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			return View("Dashboard");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}