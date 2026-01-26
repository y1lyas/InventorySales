using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IUnitOfWork uow, IMapper mapper, IUserService userService)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _uow.Repository<Product>().Query().AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
