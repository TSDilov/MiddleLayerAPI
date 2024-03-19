using Microsoft.Extensions.Logging;
using MiddleLayer.Infrastructure.Models;
using MiddleLayer.Infrastructure.Services;
using MongoDB.Driver;

namespace MiddleLayer.Infrastructure.Mutations
{
    public class DeleteCharacterMutation
    {
        private readonly ILogger<DataProviderHttpService> logger;
        private readonly MongoDbContext dbContext;

        public DeleteCharacterMutation(ILogger<DataProviderHttpService> logger, MongoDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [UseMutationConvention]
        public async Task<bool> DeleteCharacterData()
        {
            try
            {
                await this.dbContext.Characters.DeleteManyAsync(x => true);
                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred during fetching data.");
                return false;
            }
        }
    }
}
