using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Sales.Maps
{
    public class SaleItemQueryMappingProfile : Profile
    {
        public SaleItemQueryMappingProfile()
        {
            CreateMap<SaleItem, SaleItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductNameAtSale))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity.Value))
                .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.UnitPriceAtSale.Amount))
                .ForMember(d => d.LineTotal, o => o.MapFrom(s => s.LineTotal.Amount));
        }
    }
}