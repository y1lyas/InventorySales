using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using Microsoft.Extensions.Logging;

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

            product.IncreaseStock(request.Quantity, user.ExternalId);

            return Unit.Value;
        }
    }
}

