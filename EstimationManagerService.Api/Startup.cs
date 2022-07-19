using EstimationManagerService.Api.Extensions;
using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Common.Options;
using EstimationManagerService.Application.Operations.Users.Commands.CreateUser;
using EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json.Serialization;

namespace EstimationManagerService.Api;

public class Startup
{
    private IHostEnvironment _env { get; set; }
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(new HttpResponseExceptionFilter(_env));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        })
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommand>());

        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.ConfigureSwaggerGen();
        services.RegisterDependenciesInjections();
        services.AddMediatR(Assembly.GetAssembly(typeof(GetUserTasksQuery)));
        services.AddConfigurationOptions(_configuration);

        var connectionStrings = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
        services.SetupDatabase(connectionStrings.SqlDatabase);
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        _env = env;

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactReplicationService.Api v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.ConfigureExceptionHandler(app.Environment);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}