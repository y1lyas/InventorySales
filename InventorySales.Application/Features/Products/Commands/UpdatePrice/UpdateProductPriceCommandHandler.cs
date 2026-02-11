using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using InventorySales.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Commands.UpdatePrice
{
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public UpdateProductPriceCommandHandler(IUnitOfWork uow, IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(UpdateProductPriceCommand request, CancellationToken ct)
        {
            var user = await _userContext.GetCurrentUserAsync(ct);

            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            var price = Money.Create(request.NewPrice, "TRY");

            product.UpdatePrice(price , user.ExternalId);

            return Unit.Value;
        }
    }
}

