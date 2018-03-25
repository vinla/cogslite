using System;
using Microsoft.AspNetCore.Mvc;
using CogsLite.Core;
using CogsLite3.Models;
using Microsoft.AspNetCore.Http;

namespace CogsLite3.Controllers
{
    public class GamesController : Controller
    {
		private readonly IGameStore _gameStore;
		private readonly ICardStore _cardStore;
		private readonly IImageStore _imageStore;

		public GamesController(IGameStore gameStore, ICardStore cardStore, IImageStore imageStore)
		{
			_gameStore = gameStore ?? throw new ArgumentNullException(nameof(gameStore));
			_imageStore = imageStore ?? throw new ArgumentNullException(nameof(imageStore));
			_cardStore = cardStore ?? throw new ArgumentNullException(nameof(cardStore));
		}

        public IActionResult Index()
        {
			var viewModel = new GamesViewModel(_gameStore.Get(String.Empty, 0, 15));
			return View(viewModel);
        }

		public IActionResult Cards(Guid gameId)
		{
			var game = _gameStore.Get(gameId);
			var viewModel = new CardsViewModel(_cardStore.Get(gameId, String.Empty, 0, 50));
			ViewData["GameId"] = game.Id;
			ViewData["GameName"] = game.Name;
			return View(viewModel);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(string name, IFormFile image)
		{
			var game = new Game
			{
				Id = Guid.NewGuid(),
				Name = name,
				CreatedOn = DateTime.Today
			};

			_gameStore.Add(game);

			if (image != null)
				_imageStore.Store(game.Id, image.OpenReadStream());

			return RedirectToAction("Index");
		}			
    }
}