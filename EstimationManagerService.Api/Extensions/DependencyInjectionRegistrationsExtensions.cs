using EstimationManagerService.Application.Common.Helpers.MockingHelpers;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Application.Repositories.DbRepository;
using EstimationManagerService.Application.Repositories.Interfaces;

namespace EstimationManagerService.Api.Extensions;

public static class DependencyInjectionRegistrationsExtensions
{
    public static void RegisterDependenciesInjections(this IServiceCollection services)
    {
        services.AddTransient<IGuidHelper, GuidHelper>();
        services.AddScoped<IUsersDbRepository, UsersDbRepository>();
    }
}
