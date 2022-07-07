using EstimationManagerService.Api.Extensions;
using EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;
using MediatR;
using System.Reflection;
using EstimationManagerService.Application.Common.Exceptions;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;

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
.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetUserTasksQueryValidator>());
    
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterDependenciesInjections();
builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetUserTasksQuery)));


await builder.Services.SetupDatabase(config.GetValue<string>("ConnectionStrings:SqlDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(app.Environment);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();