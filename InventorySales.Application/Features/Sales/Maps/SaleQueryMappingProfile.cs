using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Sales.Maps
{
    public class SaleQueryMappingProfile : Profile
    {
        public SaleQueryMappingProfile()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice.Amount))
                .ForMember(d => d.SaleDate, o => o.MapFrom(s => s.CreatedDate))
                .ForMember(d => d.CreatedById, o => o.MapFrom(s => s.CreatedById))
                .ForMember(d => d.ItemsCount,o => o.MapFrom(s => s.Items.Count));

            CreateMap<Sale, SaleDetailDto>()
                .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice.Amount))
                .ForMember(d => d.SaleDate, o => o.MapFrom(s => s.CreatedDate))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));
        }
    }
}
