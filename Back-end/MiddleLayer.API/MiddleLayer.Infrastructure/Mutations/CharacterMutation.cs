using Microsoft.Extensions.Logging;
using MiddleLayer.Infrastructure.Inputs;
using MiddleLayer.Infrastructure.Models;
using MiddleLayer.Infrastructure.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MiddleLayer.Infrastructure.Mutations
{
    public class CharacterMutation
    {
        private readonly ILogger<DataProviderHttpService> logger;
        private readonly IMongoCollection<Character> characterCollection;

        public CharacterMutation(ILogger<DataProviderHttpService> logger, MongoDbContext dbContext)
        {
            this.logger = logger;
            this.characterCollection = dbContext.Characters;
        }

        [UseMutationConvention]
        public async Task<bool> DeleteExternalCharacterData()
        {
            try
            {
                await this.characterCollection.DeleteManyAsync(x => x.IsCreated == false);
                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred deleteing the fetch data.");
                return false;
            }
        }

        public async Task<bool> CreateCharacter(CharacterInput input)
        {
            try
            {
                var character = new Character
                {
                    Url = input.Url,
                    Name = input.Name,
                    Gender = input.Gender,
                    Born = input.Born,
                    Died = input.Died,
                    Titles = input.Titles ?? new List<string>(),
                    IsCreated = input.IsCreated,
                };

                await this.characterCollection.InsertOneAsync(character);
                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred during creating character.");
                return false;
            }
        }

        public async Task<bool> DeleteCharacter(string id)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<Character>.Filter.Eq("_id", objectId);
                var character = await this.characterCollection.FindOneAndDeleteAsync(filter);
                return character != null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred during deleting character.");
                return false;
            }
        }
    }
}
