using EstimationManagerService.Api.Extensions;
using EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;
using MediatR;
using System.Reflection;
using EstimationManagerService.Application.Common.Exceptions;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using EstimationManagerService.Application.Operations.Users.Commands.CreateUser;
using EstimationManagerService.Application.Common.Options;
using Microsoft.Extensions.Options;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(new HttpResponseExceptionFilter(builder.Environment));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        })
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommand>());

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureSwaggerGen();
        builder.Services.RegisterDependenciesInjections();
        builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetUserTasksQuery)));
        builder.Services.AddConfigurationOptions(config);

        var connectionStrings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
        await builder.Services.SetupDatabase(connectionStrings.SqlDatabase);

        var app = builder.Build();

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