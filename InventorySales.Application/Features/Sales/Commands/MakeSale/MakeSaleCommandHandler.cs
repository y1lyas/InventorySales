using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
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

            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.DecreaseStock(request.Quantity, user.ExternalId);

            var sale = new Sale(request.ProductId, request.Quantity, product.Price, user.ExternalId);

            await _uow.Repository<Sale>().AddAsync(sale);

            return sale.Id;
        }
    }

}
