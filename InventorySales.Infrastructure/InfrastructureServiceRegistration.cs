using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Infrastructure.Interceptors;
using InventorySales.Infrastructure.Persistence;
using InventorySales.Infrastructure.RedisCache;
using InventorySales.Infrastructure.Services;
using InventorySales.Infrastructure.Services.CorrelationContext;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;


namespace InventorySales.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var settings = configuration.GetSection("CacheSettings").Get<CacheSettings>();
                var opts = ConfigurationOptions.Parse(settings.ConnectionString);
                opts.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(opts);
            });

            services.AddScoped<DomainEventDispatchInterceptor>();
            services.AddScoped<AuditInterceptor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ICacheKeyGenerator, CacheKeyGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<ICorrelationContext, HttpCorrelationContext>();

            services.AddDbContext<InventoryDbContext>((sp, options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

                options.AddInterceptors(
                   sp.GetRequiredService<DomainEventDispatchInterceptor>(),
                   sp.GetRequiredService<AuditInterceptor>());
            });

            return services;
        }
    }
}
