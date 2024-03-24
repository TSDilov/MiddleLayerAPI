using Microsoft.Extensions.Logging;
using MiddleLayer.Infrastructure.Models;
using MiddleLayer.Infrastructure.Services;
using MongoDB.Driver;
using System.Text.Json;

namespace MiddleLayer.Infrastructure.Queries
{
    public class CharacterQuery
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<DataProviderHttpService> logger;
        private readonly IMongoCollection<Character> characterCollection;

        public CharacterQuery(IHttpClientFactory httpClientFactory,
            ILogger<DataProviderHttpService> logger,
            MongoDbContext dbContext)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.characterCollection = dbContext.Characters;
        }

        public async Task<Character> GetCharacterAsync()
        {
            try
            {
                var existingCharacter = await this.characterCollection.Find(x => x.IsCreated == false).FirstOrDefaultAsync();
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
    }
}
