using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Products.Maps
{
    public class ProductQueryMappingProfile : Profile
    {
        public ProductQueryMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.Price.Amount))
                .ForMember(d => d.CurrentStock, o => o.MapFrom(s => s.Stock.Value))
                .ForMember(d => d.CreatedById, o => o.MapFrom(s => s.CreatedById));
        }
    }
}
