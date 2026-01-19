using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IUnitOfWork uow, IUserContext userContext, ILogger<CreateProductCommandHandler> logger)
        {
            _uow = uow;
            _userContext = userContext;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
           "CreateProduct started ProductName={Name} Price={UnitPrice}",
           request.Name,
           request.UnitPrice);

            var user = await _userContext.GetCurrentUserAsync(cancellationToken);

            var product = new Product(request.Name, request.UnitPrice, user.ExternalId);
            await _uow.Repository<Product>().AddAsync(product);

            _logger.LogInformation(
            "CreateProduct completed ProductId={Id}",
            product.Id);

            return product.Id;
        }

    }
}
