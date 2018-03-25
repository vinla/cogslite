using System;
using System.Collections.Generic;
using System.Linq;
using CogsLite.Core;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CogsLite.MongoStore
{
	public class CardStore : BaseMongoStore, ICardStore
	{
		public CardStore(IConfiguration configuration) : base(configuration)
		{
		}

		public void Add(Card card)
		{
			var database = GetDatabase();
			var cardsCollection = database.GetCollection<Card>("Cards");
			cardsCollection.InsertOne(card);
		}

		public IEnumerable<Card> Get(Guid gameId, string nameFilter, int pageNumber, int itemsPerPage)
		{
			var database = GetDatabase();
			var cardsCollection = database.GetCollection<Card>("Cards");

			var filter = Builders<Card>.Filter.Where(c => c.GameId == gameId && c.Name.Contains(nameFilter));

			return cardsCollection.Find(filter).ToList().Skip(pageNumber * itemsPerPage).Take(itemsPerPage).ToList();
		}

		public Card Get(Guid cardId)
		{
			var database = GetDatabase();
			var cardsCollection = database.GetCollection<Card>("Cards");
			var filter = Builders<Card>.Filter.Where(c => c.Id == cardId);
			return cardsCollection.Find(filter).SingleOrDefault();
		}

		public void UpdateOne(Guid id, Action<Card> updateAction)
		{
			var database = GetDatabase();
			var cardsCollection = database.GetCollection<Card>("Cards");
			var filter = Builders<Card>.Filter.Where(g => g.Id == id);

			var game = cardsCollection.Find(filter).SingleOrDefault();
			updateAction(game);
			cardsCollection.ReplaceOne(filter, game);
		}
	}
}
