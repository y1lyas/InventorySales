using InventorySales.Application.Features.Products.Commands.CreateProduct;
using Microsoft.Extensions.DependencyInjection;

namespace InventorySales.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { },
                 typeof(CreateProductCommand).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);

            return services;
        }
    }
}
