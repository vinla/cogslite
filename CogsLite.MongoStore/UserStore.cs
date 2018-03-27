using CogsLite.Core;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace CogsLite.MongoStore
{
    public class UserStore : BaseMongoStore, IUserStore
    {
        public UserStore(IConfiguration configuration) : base(configuration)
        { }

        public void Add(User user)
        {
            var database = GetDatabase();
            var userCollection = database.GetCollection<User>("Users");
            userCollection.InsertOne(user);
        }

        public User Get(string emailAddress)
        {
            var database = GetDatabase();
            var userCollection = database.GetCollection<User>("Users");           
            var filter = Builders<User>.Filter.Where(u => u.EmailAddress == emailAddress);
            return userCollection.Find(filter).SingleOrDefault();
        }
    }
}
