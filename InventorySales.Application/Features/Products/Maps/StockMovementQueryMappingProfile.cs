using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Maps
{
    public class StockMovementQueryMappingProfile : Profile
    {
        public StockMovementQueryMappingProfile() 
        {
        
            CreateMap<StockMovement, StockMovementDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
                .ForMember(d => d.MovementType, o => o.MapFrom(s => s.MovementType))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedDate))
                .ForMember(d => d.CreatedById, o => o.MapFrom(s => s.CreatedById))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.ProductSku, o => o.MapFrom(s => s.Product.Sku.Value));

        }
    }
}
