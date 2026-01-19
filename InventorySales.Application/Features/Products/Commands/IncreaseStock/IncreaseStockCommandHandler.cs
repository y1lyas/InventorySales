using InventorySales.Application.Abstractions;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Commands.IncreaseStock
{
    public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<IncreaseStockCommandHandler> _logger;


        public IncreaseStockCommandHandler(IUnitOfWork uow, ILogger<IncreaseStockCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "IncreaseStock started ProductId={ProductId} Amount={Quantity}",
            request.ProductId,
            request.Quantity);

            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                  ?? throw new ProductNotFoundException(request.ProductId);

            product.IncreaseStock(request.Quantity);

            _logger.LogInformation(
            "IncreaseStock completed ProductId={ProductId}",
            request.ProductId);

            return Unit.Value;
        }
    }
}
