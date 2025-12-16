using InventorySales.Application.Exceptions;
using InventorySales.Application.Interfaces;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Sales.Commands.MakeSale
{
    public class MakeSaleCommandHandler : IRequestHandler<MakeSaleCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userInfo;

        public MakeSaleCommandHandler(IUnitOfWork uow, IUserService userInfo)
        {
            _uow = uow;
            _userInfo = userInfo;
        }

        public async Task<Guid> Handle(MakeSaleCommand request, CancellationToken cancellationToken)
        {
            var userId = _userInfo.UserId;

            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.ApplySale(request.Quantity);

            var sale = new Sale(request.ProductId, request.Quantity, product.UnitPrice, userId);

            await _uow.Repository<Sale>().AddAsync(sale);
            await _uow.Repository<Product>().UpdateAsync(product);

            return sale.Id;
        }
    }

}
