using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Sales.Maps
{
    public class SaleQueryMappingProfile : Profile
    {
        public SaleQueryMappingProfile()
        {
            CreateMap<Sale, SaleDto>();
        }
    }
}
