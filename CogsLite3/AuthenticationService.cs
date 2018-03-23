using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;

namespace CogsLite3
{
    public class AuthenticationService
    {
		public bool AuthenticateUser(string username, string password)
		{
			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("cogs");

			var allUsers = database.GetCollection<UserDocument>("Users").Find(FilterDefinition<UserDocument>.Empty).ToList();
			return allUsers.Any(usr => usr.Name.Equals(username, StringComparison.InvariantCultureIgnoreCase) && usr.Password == password);
		}

		public void CreateUser(string userName, string password)
		{
			var user = new UserDocument
			{
				Id = Guid.NewGuid(),
				Name = userName,
				Password = password
			};

			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("cogs");
			database.GetCollection<UserDocument>("Users").InsertOne(user);
		}
    }

	public class UserDocument
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
	}

	public class Card
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
	}
}
