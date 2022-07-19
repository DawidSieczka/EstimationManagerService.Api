using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.IO;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests;

[SetUpFixture]
public class SetupTests
{
    private static IConfiguration _configuration { get; set; }
    private static IServiceScopeFactory _scopeFactory { get; set; }
    private static Checkpoint _checkpoint { get; set; }

    [OneTimeSetUp]
    public async Task Setup()
    {
        _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

        var startup = new Api.Startup(_configuration);

        var services = new ServiceCollection();
        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" &&
            w.ApplicationName == "EstimationManagerService.Api"));

        startup.ConfigureServices(services);

        _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" },
        };

        await EnsureDatabaseAsync();
    }

    public static async Task EnsureDatabaseAsync()
    {
        using var scope = _scopeFactory.CreateAsyncScope();

        using var context = scope.ServiceProvider.GetRequiredService<Persistance.AppDbContext>();
        await context.Database.MigrateAsync();
    }

    public static async Task ResetState()
    {
        await _checkpoint.Reset(_configuration.GetConnectionString("SqlDatabase"));
    }
}