using InventorySales.API.Extensions;
using InventorySales.API.Middlewares.Audit;
using InventorySales.API.Middlewares.Auth;
using InventorySales.API.Middlewares.GlobalException;
using InventorySales.Application;
using InventorySales.Infrastructure;
using InventorySales.Infrastructure.RedisCache;
using Microsoft.Extensions.Options;
using Serilog;
using StackExchange.Redis;
using System.Diagnostics;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddSwaggerGenWithAuth(builder.Configuration)
    .AddAuthConfiguration(builder.Configuration)
    .AddRateLimitConiguration(builder.Configuration)
    .AddMediatRPipelineBehavior();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();


builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection("CacheSettings"));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var settings = sp
      .GetRequiredService<IOptions<CacheSettings>>()
      .Value;

    var configuration = ConfigurationOptions.Parse(settings.ConnectionString);
    configuration.AbortOnConnectFail = false;
    return ConnectionMultiplexer.Connect(configuration);
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId("public-client");
        c.OAuthUsePkce();
    });
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<CorrelationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserSyncMiddleware>();

app.UseRateLimiter();
app.MapControllers();
app.Run();
