using Microsoft.AspNetCore.Mvc;
using SessionCookie.MVC.Filters;
using SessionCookie.MVC.Models;
using System.Diagnostics;

namespace SessionCookie.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [SessionAuthorize]
        public IActionResult Index()
        {
            /*if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login","Account");
            }*/
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
