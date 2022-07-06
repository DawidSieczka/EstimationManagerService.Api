using EstimationManagerService.Api.Extensions;
using EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();