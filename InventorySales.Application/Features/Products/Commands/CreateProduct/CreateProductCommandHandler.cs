using InventorySales.Application.Abstractions;
using InventorySales.Application.Common.Exceptions;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;
using InventorySales.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken ct)
        {
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

            var product = new Product(request.Sku, request.Name, price, request.CategoryId);

            await _uow.Repository<Product>().AddAsync(product);

            return _mapper.Map<ProductDto>(product);
        }

    }
}
