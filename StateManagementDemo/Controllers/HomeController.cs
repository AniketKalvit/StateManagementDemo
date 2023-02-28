using Microsoft.AspNetCore.Mvc;
using StateManagementDemo.Models;
using System.Diagnostics;

namespace StateManagementDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        // working with cookie
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            string email = form["email"];
            //set cookie
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(60);
            options.Path = "/";
            //options.Secure = true;
            //options.HttpOnly = true; // cookie can be read using client side script --> javascript / vbscript
            // Response property is used to write to cookie
            _httpContextAccessor.HttpContext.Response.Cookies.Append("email", email, options);
           
            return RedirectToAction("ReadCookie", "ReadCookieSession");
        }
        public IActionResult WorkingWithSession()
        {
            return View();
        }
        [HttpPost]
        public IActionResult WorkingWithSession(IFormCollection form)
        {
            string email = form["email"];
            HttpContext.Session.SetString("email", email);
            HttpContext.Session.SetString("roleId", "2");
            return RedirectToAction("ReadSession", "ReadCookieSession");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}