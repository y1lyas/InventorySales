using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Behaviors;
using InventorySales.Application.Common;
using InventorySales.Infrastructure.Behaviours;
using InventorySales.Infrastructure.Persistence;
using InventorySales.Infrastructure.RedisCache;
using InventorySales.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InventoryDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["CacheSettings:ConnectionString"];
                options.InstanceName = configuration["CacheSettings:InstanceName"];
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ICacheKeyGenerator, CacheKeyGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserContext, UserContext>();

            return services;
        }
    }
}
