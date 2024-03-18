using Microsoft.Extensions.Configuration;
using MiddleLayer.Infrastructure.Models;
using MongoDB.Driver;

namespace MiddleLayer.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase database;
        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");
            var client = new MongoClient(connectionString);
            this.database = client.GetDatabase("MiddleLayer");
        }

        public IMongoCollection<Character> Characters => this.database.GetCollection<Character>("characters");
    }
}
