using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Common.Exceptions;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            var productDto = await _uow.Repository<Product>()
         .Query()
         .AsNoTracking()
         .Where(x => x.Id == request.ProductId)
         .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
         .FirstOrDefaultAsync(ct);

            if (productDto == null)
                throw new ProductNotFoundException(request.ProductId);

            return productDto;
        }
    }
}
