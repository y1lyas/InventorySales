using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Sales.Maps
{
    public class SaleQueryMappingProfile : Profile
    {
        public SaleQueryMappingProfile()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity.Value))
                .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice.Amount))
                .ForMember(d => d.SaleDate, o => o.MapFrom(s => s.CreatedDate))
                .ForMember(d => d.CreatedById, o => o.MapFrom(s => s.CreatedById));
        }
    }
}
