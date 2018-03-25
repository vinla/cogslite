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

		public Game Get(Guid gameId)
		{
			var database = GetDatabase();
			var gamesCollection = database.GetCollection<Game>("Games");
			var filter = Builders<Game>.Filter.Where(g => g.Id == gameId);
			return gamesCollection.Find(filter).SingleOrDefault();
		}

		public IEnumerable<Game> Get(string nameFilter, int pageNumber, int itemsPerPage)
		{
			var database = GetDatabase();
			var gamesCollection = database.GetCollection<Game>("Games");

			var filter = Builders<Game>.Filter.Where(g => g.Name.Contains(nameFilter));

			return gamesCollection.Find(filter).ToList().Skip(pageNumber * itemsPerPage).Take(itemsPerPage).ToList();
		}		

		public void UpdateOne(Guid id, Action<Game> updateAction)
		{
			var database = GetDatabase();
			var gamesCollection = database.GetCollection<Game>("Games");
			var filter = Builders<Game>.Filter.Where(g => g.Id == id);
			
			var game = gamesCollection.Find(filter).SingleOrDefault();
			updateAction(game);
			gamesCollection.ReplaceOne(filter, game);
		}
	}
}
