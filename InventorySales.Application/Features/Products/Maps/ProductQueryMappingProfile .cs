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
                .ForMember(d => d.CreatedById, o => o.MapFrom(s => s.CreatedById))
                .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.CategoryId))
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.SKUnit, o => o.MapFrom(s => s.Sku.Value))
                .ForMember(d => d.Currency, o => o.MapFrom(s => s.Price.Currency))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedDate));
        }
    }
}
