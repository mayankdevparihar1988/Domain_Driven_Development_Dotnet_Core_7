
using Application;
using Infrastructure;
using WebApi;
using WebApi.Middlewares;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

var cacheSettings = builder.Services.GetCacheSettings(builder.Configuration);

//Configure Redis

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = cacheSettings?.DestinationUrl;
});

var app = builder.Build();

// Global ErrorHandler

app.UseGlobalErrorHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option => option.DisplayRequestDuration());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

