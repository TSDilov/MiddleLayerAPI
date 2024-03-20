using Microsoft.Extensions.Configuration;
using MiddleLayer.Identity.Models;
using MiddleLayer.Tests.Helpers;
using MongoDB.Driver;
using System.Net.Http.Headers;
using static MiddleLayer.Tests.Testing;

namespace MiddleLayer.Tests
{
    public abstract class TestBase
    {
        protected const string DefaultUserEmail = "testLogin@gmail.com";
        protected const string DefaultUserPassword = "Qwerty123@";
        private IMongoDatabase mongoDatabase;
        private MongoClient mongoClient;

        protected TestBase()
        {
            HttpClient = new MiddleLayerHttpClient(Client);
        }

        public MiddleLayerHttpClient HttpClient { get; private set; }

        [SetUp]
        public async Task TestSetup()
        {
            await SeedAndAuthenticate();

            var mongoConnectionString = Configuration.GetConnectionString("MongoDB");
            var databaseName = "MiddleLayer";

           this.mongoClient = new MongoClient(mongoConnectionString);
           this.mongoDatabase = this.mongoClient.GetDatabase(databaseName);

        }

        public async Task SeedDefaultUser()
        {
            await HttpClient.Register(new RegistrationRequest()
            {
                Email = DefaultUserEmail,
                FirstName = "User",
                Password = DefaultUserPassword,
                LastName = "LastName",
                UserName = "username"
            });
        }

        public async Task SeedAndAuthenticate()
        {
            await SeedDefaultUser();

            var response = await HttpClient.Login(new AuthRequest()
            {
                Email = DefaultUserEmail,
                Password = DefaultUserPassword
            });

            HttpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Success.Token);
        }

        [TearDown]
        public void TearDown()
        {

            this.mongoClient = null;
            this.mongoDatabase = null;
        }
    }
}
