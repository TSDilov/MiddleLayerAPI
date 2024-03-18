using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiddleLayer.Infrastructure.Contracts;
using MiddleLayer.Infrastructure.Services;

namespace MiddleLayer.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataProviderHttpService, DataProviderHttpService>();

            services.AddSingleton(provider => new MongoDbContext(configuration));
            return services;
        }
    }
}
