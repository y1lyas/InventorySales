using InventorySales.Application.Features.Dashboard.DTOs;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;

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
                .ForMember(d => d.ProductSku, o => o.MapFrom(s => s.Product.Sku.Value))
                .ForMember(d => d.Reason, o => o.MapFrom(s => s.Reason))
                .ForMember(d => d.SaleReferenceId, o => o.MapFrom(s => s.SaleReferenceId));

            CreateMap<StockMovement, RecentStockMovementDto>()
          .ForMember(d => d.ProductName,
              o => o.MapFrom(s => s.Product.Name))
          .ForMember(d => d.CreatedDate,
              o => o.MapFrom(s => s.CreatedDate));

        }
    }
}
