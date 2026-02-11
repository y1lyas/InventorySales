using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Sales.Commands.MakeSale
{
    public class MakeSaleCommandHandler : IRequestHandler<MakeSaleCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;


        public MakeSaleCommandHandler(IUnitOfWork uow, IUserContext userContext )
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(MakeSaleCommand request, CancellationToken ct)
        {
            var user = await _userContext.GetCurrentUserAsync(ct);
            var sale = new Sale(user.ExternalId);
            foreach (var itemRequest in request.Items)
            {
                var product = await _uow.Repository<Product>().GetByIdAsync(itemRequest.ProductId)
                    ?? throw new ProductNotFoundException(itemRequest.ProductId);
                sale.AddItem(product, itemRequest.Quantity);
            }
            await _uow.Repository<Sale>().AddAsync(sale);

            return sale.Id;
        }
    }

}
