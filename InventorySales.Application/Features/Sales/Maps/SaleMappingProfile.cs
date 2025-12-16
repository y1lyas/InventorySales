using InventorySales.Application.Features.Sales.Commands.MakeSale;
using InventorySales.Application.Features.Sales.DTOs;

namespace InventorySales.Application.Features.Sales.Maps
{
    public class SaleMappingProfile : Profile
    {
        public SaleMappingProfile()
        {
            CreateMap<MakeSaleDto, MakeSaleCommand>();
        }
    }
}
