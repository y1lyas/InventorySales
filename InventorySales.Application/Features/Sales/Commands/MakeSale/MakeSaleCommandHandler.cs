using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Exceptions;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Enums;
using InventorySales.Domain.ValueObjects;

namespace InventorySales.Application.Features.Sales.Commands.MakeSale
{
    public class MakeSaleCommandHandler : IRequestHandler<MakeSaleCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public MakeSaleCommandHandler(IUnitOfWork uow, IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(MakeSaleCommand request, CancellationToken ct)
        {
            var user = await _userContext.GetCurrentUserAsync(ct);
            var currency = "TRY";
            var sale = new Sale(currency);
            var context = new StockMovementContext(
            user.ExternalId,
            MovementReason.Sale,
            sale.Id);

            foreach (var itemRequest in request.Items)
            {
                var product = await _uow.Repository<Product>().GetByIdAsync(itemRequest.ProductId)
                    ?? throw new ProductNotFoundException(itemRequest.ProductId);

                product.DecreaseStock(itemRequest.Quantity, context);

                sale.AddItem(product, itemRequest.Quantity);
            }
            await _uow.Repository<Sale>().AddAsync(sale);

            sale.Complete();

            return sale.Id;
        }
    }

}
