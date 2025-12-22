using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userInfo;
        public CreateProductCommandHandler(IUnitOfWork uow, IUserService userInfo)
        {
            _uow = uow;
            _userInfo = userInfo;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var userId = _userInfo.UserId;

            var product = new Product(request.Name, request.UnitPrice, userId);
            await _uow.Repository<Product>().AddAsync(product);
            return product.Id;
        }

    }
}
