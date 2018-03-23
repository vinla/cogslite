using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CogsLite3.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using CogsLite.Core;

namespace CogsLite3.Controllers
{
    public class HomeController : Controller
    {
		private readonly IGameStore _gameStore;

		public HomeController(IGameStore gameStore)
		{
			_gameStore = gameStore ?? throw new ArgumentNullException(nameof(gameStore));
		}

        public IActionResult Index()
        {
			var viewModel = new GamesViewModel(_gameStore.Get(String.Empty, 0, 15));			
            return View(viewModel);
        }

        public IActionResult Home()
        {
            return View("About");
        }        

		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			var authService = new AuthenticationService();
			
			if(authService.AuthenticateUser(username, password))
				return RedirectToAction(nameof(Home));

			return RedirectToAction(nameof(Error));
		}

		public IActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Upload(int rows, int columns, List<IFormFile> files)
		{
			var cardStore = new CardStore();

			foreach (var file in files)
			{
				using (var dataStream = file.OpenReadStream())
				{
					foreach(var image in ImageSlicer.Slice(rows, columns, dataStream))
					{
						cardStore.StoreNew("Basic", image);
					}
				}
			}
			return RedirectToAction("Cards");
		}

		public IActionResult Cards()
		{
			var cardStore = new CardStore();
			var cards = cardStore.RetrieveAll();
			var viewModel = new CardsViewModel(cards);
			return View(viewModel);
		}

		public IActionResult CardImage(string cardId)
		{
			var cardStore = new CardStore();
			return new FileContentResult(cardStore.GetCardImage(cardId), "image/png");
		}

		public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
