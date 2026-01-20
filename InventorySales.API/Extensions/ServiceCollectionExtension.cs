using InventorySales.Application.Behaviors;
using InventorySales.Application.Common;
using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Infrastructure.Behaviours;
using InventorySales.Infrastructure.RedisCache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Threading.RateLimiting;

namespace InventorySales.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                c.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(config["Keycloak:AuthorizationUrl"]!),
                            TokenUrl = new Uri(config["Keycloak:TokenUrl"]!),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "openid" },
                                { "profile", "profile" }
                            }
                        }
                    }
                });

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Keycloak"
                            },
                            In = ParameterLocation.Header,
                            Name = "Bearer",
                            Scheme = "Bearer"
                        },
                        []
                    }
                };

                c.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(o =>
              {
                 o.RequireHttpsMetadata = false;
                 o.Audience = config["Authentication:Audience"];
                 o.MetadataAddress = config["Authentication:MetadataAddress"]!;
                 o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
              {
                  ValidIssuer = config["Authentication:ValidIssuer"],
                  RoleClaimType = ClaimTypes.Role,
                  NameClaimType = ClaimTypes.NameIdentifier
                 };
              }
              );
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ProductCreate", policy =>
                    policy.RequireRole("product.create"));
                options.AddPolicy("StockIncrease", policy =>
                    policy.RequireRole("stock.increase"));
                options.AddPolicy("StockDecrease", policy =>
                  policy.RequireRole("stock.decrease"));
                options.AddPolicy("StockReadLow", policy =>
                  policy.RequireRole("stock.read_low"));
                options.AddPolicy("SaleCreate", policy =>
                  policy.RequireRole("sale.create"));
                options.AddPolicy("SaleReadOwn", policy =>
                  policy.RequireRole("sale.read.own"));
                options.AddPolicy("ProductUpdatePrice", policy =>
                  policy.RequireRole("product.update.price"));
                options.AddPolicy("ProductRead", policy =>
                  policy.RequireRole("product.read"));

            });

            return services;
        }
        public static IServiceCollection AddRateLimitConiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy("read-policy", context =>
                  RateLimitPartition.GetFixedWindowLimiter(
                  partitionKey: $"read:{context.Connection.RemoteIpAddress}",
                  factory: _ => new FixedWindowRateLimiterOptions
                  {
                    PermitLimit = 100,
                    Window = TimeSpan.FromMinutes(1)
                  }));
                options.AddPolicy("write-policy", context =>
                {
                    var userId =
                        context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (!string.IsNullOrEmpty(userId))
                    {
                        return RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: $"user:{userId}",
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 10,
                                Window = TimeSpan.FromMinutes(1),
                                QueueLimit = 0
                            });
                    }

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: $"ip:{context.Connection.RemoteIpAddress}",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        });
                });

            });

            return services;
        }
        public static IServiceCollection AddMediatRPipelineBehavior(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);

                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            });

            return services;
        }
    }
}

