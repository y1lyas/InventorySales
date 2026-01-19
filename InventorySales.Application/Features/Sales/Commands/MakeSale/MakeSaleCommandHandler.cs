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
        private readonly ILogger<MakeSaleCommandHandler> _logger;


        public MakeSaleCommandHandler(IUnitOfWork uow, IUserContext userContext, ILogger<MakeSaleCommandHandler> logger)
        {
            _uow = uow;
            _userContext = userContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(MakeSaleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
         "MakeSale started ProductId={ProductId} Amount={Quantity}",
         request.ProductId,
         request.Quantity);

            var user = await _userContext.GetCurrentUserAsync(cancellationToken);

            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.ApplySale(request.Quantity);

            var sale = new Sale(request.ProductId, request.Quantity, product.UnitPrice, user.ExternalId);

            await _uow.Repository<Sale>().AddAsync(sale);
            await _uow.Repository<Product>().UpdateAsync(product);

            _logger.LogInformation(
            "MakeSale completed ProductId={ProductId} SaleId={Id}",
            request.ProductId,
            sale.Id);

            return sale.Id;
        }
    }

}
