using InventorySales.Application.Abstractions;
using InventorySales.Application.Common.Exceptions;
using InventorySales.Domain.Entities;
using InventorySales.Domain.ValueObjects;

namespace InventorySales.Application.Features.Products.Commands.UpdatePrice
{
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public UpdateProductPriceCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(UpdateProductPriceCommand request, CancellationToken ct)
        {
            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            var price = Money.Create(request.NewPrice, "TRY");

            product.UpdatePrice(price);

            return Unit.Value;
        }
    }
}

