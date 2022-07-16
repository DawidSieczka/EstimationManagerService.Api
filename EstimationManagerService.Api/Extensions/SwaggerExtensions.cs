using System;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace EstimationManagerService.Api.Extensions;

public static class SwaggerExtensions
{
    public static void ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Estimation Manager Service API",
                Description = "Estimation manager service API. The service is responsible for backend tasks management with clockify integration. The API bases on .NET 6 and MS SQL communication on code-first approach.",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
