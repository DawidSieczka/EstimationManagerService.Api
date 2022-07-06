using EstimationManagerService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Api.Extensions;

public static class DbSetupExtensions
{
    public async static Task SetupDatabase(this IServiceCollection services, string sqlDbConnectionString)
    {
        services.AddDbContext<AppDbContext>(b => b
            .UseLazyLoadingProxies()
            .UseSqlServer(sqlDbConnectionString, options => options.CommandTimeout(120)
        ));

        using var appDbContext = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
        await appDbContext.Database.MigrateAsync();
    }
}
