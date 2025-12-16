using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;
namespace InventorySales.Application.Features.Products.Maps
{
    public class ProductQueryMappingProfile : Profile
    {
        public ProductQueryMappingProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
