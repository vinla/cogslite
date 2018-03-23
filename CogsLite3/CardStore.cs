using System;
using System.Collections.ObjectModel;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace CogsLite3
{
    public class CardStore
    {
		public void StoreNew(string type, byte[] imageData)
		{
			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("cogs");									

			var card = new Card
			{
				Id = Guid.NewGuid(),
				Name = String.Empty,
				Type = type
			};			

			database.GetCollection<Card>("Cards").InsertOne(card);
			var fileStorage = new GridFSBucket(database, new GridFSBucketOptions
			{
				BucketName = "card_images"
			});
			fileStorage.UploadFromBytes(card.Id.ToString(), imageData);
		}

		public ReadOnlyCollection<Card> RetrieveAll()
		{
			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("cogs");
			return database.GetCollection<Card>("Cards").Find(FilterDefinition<Card>.Empty).ToList().AsReadOnly();
		}

		public byte[] GetCardImage(string cardId)
		{
			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("cogs");
			var fileStorage = new GridFSBucket(database, new GridFSBucketOptions
			{
				BucketName = "card_images"
			});
			return fileStorage.DownloadAsBytesByName(cardId);
		}
    }
}
