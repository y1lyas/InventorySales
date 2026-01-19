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
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IUnitOfWork uow, IMapper mapper, IUserService userService, ILogger<CreateProductCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllProducts requested");

            var products = await _uow.Repository<Product>().Query().AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
