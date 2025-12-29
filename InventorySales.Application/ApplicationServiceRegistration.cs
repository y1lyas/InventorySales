using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

            services.AddAutoMapper(cfg => { },
                 typeof(CreateProductCommand).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);

            services.AddScoped<IPasswordHasher, PasswordHasher>();



            return services;
        }
    }
}
