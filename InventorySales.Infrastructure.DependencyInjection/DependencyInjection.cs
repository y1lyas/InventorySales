using FluentValidation;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Behaviors;
using InventorySales.Application.Common;
using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Services;
using InventorySales.Infrastructure.Behaviours;
using InventorySales.Infrastructure.Persistence;
using InventorySales.Infrastructure.RedisCache;
using InventorySales.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace InventorySales.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<InventoryDbContext>(opt =>
                opt.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

            services.AddAutoMapper(cfg => { },
                 typeof(CreateProductCommand).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config["CacheSettings:ConnectionString"];
                options.InstanceName = config["CacheSettings:InstanceName"];
            });

            services.AddTransient<ICacheService, CacheService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));



            return services;
        }
    }
}
