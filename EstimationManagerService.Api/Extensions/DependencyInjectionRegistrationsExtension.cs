using EstimationManagerService.Application.Services;
using EstimationManagerService.Application.Services.Interfaces;

namespace EstimationManagerService.Api.Extensions;

public static class DependencyInjectionRegistrationsExtension
{
    public static void RegisterDependenciesInjections(this IServiceCollection services)
    {
        services.AddTransient<IGuidService, GuidService>();
    }
}
