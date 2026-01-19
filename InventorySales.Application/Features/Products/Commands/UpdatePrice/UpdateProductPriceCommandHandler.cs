using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Commands.UpdatePrice
{
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;
        private readonly ILogger<UpdateProductPriceCommandHandler> _logger;

        public UpdateProductPriceCommandHandler(IUnitOfWork uow, IUserContext userContext, ILogger<UpdateProductPriceCommandHandler> logger)
        {
            _uow = uow;
            _userContext = userContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "UpdatePrice started ProductId={ProductId} NewPrice={NewPrice}",
            request.ProductId,
            request.NewPrice);

            var user = await _userContext.GetCurrentUserAsync(cancellationToken);

            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.UpdatePrice(request.NewPrice, user.ExternalId);

            _logger.LogInformation(
            "UpdatePrice completed ProductId={ProductId} CurrentPrice={NewPrice}",
            request.ProductId,
            request.NewPrice);

            return Unit.Value;
        }
    }
}
