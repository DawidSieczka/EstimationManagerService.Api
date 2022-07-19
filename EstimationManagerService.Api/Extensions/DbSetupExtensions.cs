using EstimationManagerService.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Api.Extensions;

public static class DbSetupExtensions
{
    public static void SetupDatabase(this IServiceCollection services, string sqlDbConnectionString)
    {
        services.AddDbContext<AppDbContext>(b => b
            .UseLazyLoadingProxies()
            .UseSqlServer(sqlDbConnectionString, options => options.CommandTimeout(120)
            .EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
        ));

        using var appDbContext = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
        appDbContext.Database.Migrate();
    }
}
