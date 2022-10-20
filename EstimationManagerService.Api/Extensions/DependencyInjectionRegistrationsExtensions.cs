using EstimationManagerService.Application.Common.Helpers.MockingHelpers;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;

namespace EstimationManagerService.Api.Extensions;

public static class DependencyInjectionRegistrationsExtensions
{
    public static void RegisterDependenciesInjections(this IServiceCollection services)
    {
        services.AddTransient<IGuidHelper, GuidHelper>();        
    }
}
