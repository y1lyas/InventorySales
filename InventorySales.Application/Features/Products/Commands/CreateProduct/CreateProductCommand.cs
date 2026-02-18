
using InventorySales.Application.Features.Interfaces;

namespace InventorySales.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Sku,string Name, decimal UnitPrice, Guid? CategoryId = null) : IRequest<Guid>, ICommand;

}
