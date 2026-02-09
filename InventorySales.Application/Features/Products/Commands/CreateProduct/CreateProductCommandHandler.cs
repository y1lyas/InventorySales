using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public CreateProductCommandHandler(IUnitOfWork uow, IUserContext userContext)
        {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
        {
            var user = await _userContext.GetCurrentUserAsync(ct);

            var product = new Product(request.Name, request.UnitPrice, user.ExternalId);

            await _uow.Repository<Product>().AddAsync(product);

            return product.Id;
        }

    }
}
