
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Products.DTOs;

namespace InventorySales.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Sku,string Name, decimal UnitPrice, Guid? CategoryId = null) : IRequest<ProductDto>, ICommand;

}
