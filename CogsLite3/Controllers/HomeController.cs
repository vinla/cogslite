using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CogsLite3.Models;
using Microsoft.AspNetCore.Http;

namespace CogsLite3.Controllers
{
    public class HomeController : Controller
    {
		public HomeController()
		{
		}

        public IActionResult Index()
        {
			return View();
        }
        
        public IActionResult About()
        {
            return View();
        }

		public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
