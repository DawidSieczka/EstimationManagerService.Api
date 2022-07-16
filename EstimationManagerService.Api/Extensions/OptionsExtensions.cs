using System;
using EstimationManagerService.Application.Common.Options;

namespace EstimationManagerService.Api.Extensions;

public static class OptionsExtensions
{
    public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));
    }
}
