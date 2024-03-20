using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiddleLayer.Tests.Helpers;
using Testcontainers.PostgreSql;

namespace MiddleLayer.Tests
{
    [SetUpFixture]
    public class Testing
    {
        private PostgreSqlContainer postgres;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTest() 
        {
            this.postgres = new PostgreSqlBuilder()
                .WithImage("postgres")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .WithPortBinding(9999, 5432)
                .Build();

            await this.postgres.StopAsync();

            var application = new CustomWebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder
                .UseEnvironment("CI")
                .ConfigureServices((ctx, services) =>
                {
                    Configuration = ctx.Configuration;
                    ScopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
                })
            );

            Client = application.CreateClient();
        }

        [OneTimeTearDown]
        public async Task RunAfterAllTests()
        {
            await this.postgres.DisposeAsync();
        }

        public static IConfiguration Configuration { get; private set; }

        public static IServiceScopeFactory ScopeFactory { get; private set; }

        public static HttpClient Client { get; private set; }
    }
}
