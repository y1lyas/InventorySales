using InventorySales.Application.Exceptions;
using InventorySales.Application.Interfaces;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Products.Commands.UpdatePrice
{
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userInfo;

        public UpdateProductPriceCommandHandler(IUnitOfWork uow, IUserService userInfo)
        {
            _uow = uow;
            _userInfo = userInfo;
        }

        public async Task<Unit> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var userId = _userInfo.UserId;

            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.UpdatePrice(request.NewPrice, userId);

            return Unit.Value;
        }
    }
}
