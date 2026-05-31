using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Exceptions;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Enums;
using InventorySales.Domain.ValueObjects;

namespace InventorySales.Application.Features.Products.Commands.IncreaseStock
{
    public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public IncreaseStockCommandHandler(IUnitOfWork uow, IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(IncreaseStockCommand request, CancellationToken ct)
        {
            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                  ?? throw new ProductNotFoundException(request.ProductId);

            var user = await _userContext.GetCurrentUserAsync(ct);

            var context = new StockMovementContext(
           user.ExternalId,
           MovementReason.Adjustment);

            product.IncreaseStock(request.Quantity, context);

            return Unit.Value;
        }
    }
}

