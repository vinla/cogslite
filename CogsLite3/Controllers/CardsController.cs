using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CogsLite3.Controllers
{
    public class CardsController : Controller
    {		
        public IActionResult Index(Guid gameId)
        {
            return View();
        }

		public IActionResult Upload(Guid gameId, int cardCount, int rows, int columns, List<IFormFile> files)
		{
			throw new NotImplementedException();
		}
    }
}