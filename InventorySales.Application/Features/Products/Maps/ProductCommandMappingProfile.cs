using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.Commands.DecreaseStock;
using InventorySales.Application.Features.Products.Commands.IncreaseStock;
using InventorySales.Application.Features.Products.Commands.UpdatePrice;
using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Maps
{
    public class ProductCommandMappingProfile : Profile
    {
        public ProductCommandMappingProfile()
        {
            CreateMap<CreateProductDto, CreateProductCommand>();
            CreateMap<IncreaseStockDto, IncreaseStockCommand>();
            CreateMap<DecreaseStockDto, DecreaseStockCommand>();
            CreateMap<UpdatePriceDto, UpdateProductPriceCommand>();
        }
    }
}
