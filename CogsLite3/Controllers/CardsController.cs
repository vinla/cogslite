using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CogsLite.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CogsLite3.Controllers
{
    public class CardsController : Controller
    {
		private readonly IGameStore _gameStore;
		private readonly ICardStore _cardStore;
		private readonly IImageStore _imageStore;

		public CardsController(IGameStore gameStore, ICardStore cardStore, IImageStore imageStore)
		{
			_gameStore = gameStore ?? throw new ArgumentNullException(nameof(gameStore));
			_cardStore = cardStore ?? throw new ArgumentNullException(nameof(cardStore));
			_imageStore = imageStore ?? throw new ArgumentNullException(nameof(imageStore));
		}

        public IActionResult Index(Guid gameId)
        {
            return View();
        }

		public IActionResult Upload(Guid gameId)
		{
			var game = _gameStore.Get(gameId);
			ViewData["GameId"] = gameId;
			ViewData["GameName"] = game.Name;
			return View();
		}

		[HttpPost]
		public IActionResult Upload(Guid gameId, int cardsPerRow, int cardCount, IFormFile cardSheet)
		{			
			foreach(var imageData in ImageSlicer.Slice(cardsPerRow, cardCount, cardSheet.OpenReadStream()))
			{				
				var card = new Card
				{
					Id = Guid.NewGuid(),
					Name = String.Empty,
					GameId = gameId,
					CreatedOn = DateTime.Now
				};

				_cardStore.Add(card);
				_imageStore.Store(card.Id, imageData);
			}
			
			_gameStore.UpdateOne(gameId, g => g.CardCount += cardCount);
			return RedirectToAction("Cards", "Games", new { gameId });
		}

		public IActionResult Details(Guid cardId)
		{
			var card = _cardStore.Get(cardId);
			return View(card);
		}

		[HttpPost]
		public IActionResult UpdateDetails(Guid gameId, Guid cardId, string name)
		{
			_cardStore.UpdateOne(cardId, c => c.Name = name);
			return RedirectToAction("Cards", "Games", new { gameId });
		}
    }
}