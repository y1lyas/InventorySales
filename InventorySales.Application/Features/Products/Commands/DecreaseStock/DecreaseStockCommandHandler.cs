using InventorySales.Application.Abstractions;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Products.Commands.DecreaseStock
{
    public class DecreaseStockCommandHandler : IRequestHandler<DecreaseStockCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DecreaseStockCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.DecreaseStock(request.Quantity);

            return Unit.Value;
        }
    }
}
