using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.RedisCache;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Exceptions;
using InventorySales.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Threading;

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

            if (request.CategoryId.HasValue)
            {
                var categoryExists = await _uow.Repository<Category>().Query()
                    .AnyAsync(x => x.Id == request.CategoryId.Value, ct);

                if (!categoryExists)
                {
                    throw new CategoryNotFoundException(request.CategoryId);
                }
            }
            var price = Money.Create(request.UnitPrice, "TRY");

            var product = new Product(request.Sku ,request.Name,price, user.ExternalId);

            await _uow.Repository<Product>().AddAsync(product);

            return product.Id;
        }

    }
}
