using System;
using System.Collections.Generic;
using System.Linq;
using CogsLite.Core;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace CogsLite.MongoStore
{
	public class GameStore : BaseMongoStore, IGameStore
	{		
		public GameStore(IConfiguration configuration) : base(configuration)
		{			
		}

		public void Add(Game game)
		{
			var database = GetDatabase();
			var gamesCollection = database.GetCollection<Game>("Games");

			var filter = Builders<Game>.Filter.Where(g => g.Name == game.Name);
			if (gamesCollection.Find(filter).Any())
				throw new InvalidOperationException("A game with that name already exists");

			gamesCollection.InsertOne(game);
		}

		public IEnumerable<Game> Get(string nameFilter, int pageNumber, int itemsPerPage)
		{
			var database = GetDatabase();
			var gamesCollection = database.GetCollection<Game>("Games");

			var filter = Builders<Game>.Filter.Where(g => g.Name.Contains(nameFilter));

			return gamesCollection.Find(filter).ToList().Skip(pageNumber * itemsPerPage).Take(itemsPerPage).ToList();
		}		
	}
}
