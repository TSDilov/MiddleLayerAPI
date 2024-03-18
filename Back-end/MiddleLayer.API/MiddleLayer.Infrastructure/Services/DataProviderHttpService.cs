using Microsoft.Extensions.Logging;
using MiddleLayer.Infrastructure.Contracts;
using MiddleLayer.Infrastructure.Models;
using MongoDB.Driver;
using System.Text.Json;

namespace MiddleLayer.Infrastructure.Services
{
    public class DataProviderHttpService : IDataProviderHttpService
    {

        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<DataProviderHttpService> logger;
        private readonly IMongoCollection<Character> characterCollection;

        public DataProviderHttpService(
            IHttpClientFactory httpClientFactory, 
            ILogger<DataProviderHttpService> logger,
            MongoDbContext dbContext)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.characterCollection = dbContext.Characters;
        }

        public async Task<Character> GetData()
        {
            try
            {

                // Check if the character already exists in the collection
                var existingCharacter = await this.characterCollection.Find(x => true).FirstOrDefaultAsync();
                if (existingCharacter != null)
                {
                    return existingCharacter;
                }

                var client = this.httpClientFactory.CreateClient(ApiPaths.MiddleLayerApiName);
                var response = await client.GetAsync("");
                var jsonContent = await response.Content.ReadAsStringAsync();
                var character = JsonSerializer.Deserialize<Character>(jsonContent);
                await this.characterCollection.InsertOneAsync(character);
                return character;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred during fetching data.");
                throw;
            }
        }

        public async Task DeleteData()
        {
            try
            {
                await this.characterCollection.DeleteManyAsync(x => true);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred during data deletion.");
                throw;
            }
        }
    }
}
