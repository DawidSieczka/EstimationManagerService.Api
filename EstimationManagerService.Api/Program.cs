using EstimationManagerService.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(b => b
            .UseLazyLoadingProxies()
            .UseSqlServer(config.GetValue<string>("ConnectionStrings:SqlDb"))
        );

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