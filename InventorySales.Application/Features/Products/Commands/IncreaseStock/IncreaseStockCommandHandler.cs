using InventorySales.Application.Exceptions;
using InventorySales.Application.Interfaces;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Products.Commands.IncreaseStock
{
    public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public IncreaseStockCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                  ?? throw new ProductNotFoundException(request.ProductId);


            product.IncreaseStock(request.Quantity);

            return Unit.Value;
        }
    }
}
