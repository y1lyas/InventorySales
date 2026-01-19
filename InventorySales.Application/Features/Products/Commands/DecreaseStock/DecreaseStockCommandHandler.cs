using InventorySales.Application.Abstractions;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Commands.DecreaseStock
{
    public class DecreaseStockCommandHandler : IRequestHandler<DecreaseStockCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DecreaseStockCommandHandler> _logger;


        public DecreaseStockCommandHandler(IUnitOfWork uow, ILogger<DecreaseStockCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
         "DecreaseStock started ProductId={ProductId} Amount={Quantity}",
         request.ProductId,
         request.Quantity);

            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.DecreaseStock(request.Quantity);

            _logger.LogInformation(
           "DecreaseStock completed ProductId={ProductId}",
           request.ProductId);

            return Unit.Value;
        }
    }
}
