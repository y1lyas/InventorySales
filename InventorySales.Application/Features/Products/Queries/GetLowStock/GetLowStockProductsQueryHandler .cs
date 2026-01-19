using InventorySales.Application.Abstractions;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventorySales.Application.Features.Products.Queries.GetLowStock
{
    public class GetLowStockProductsQueryHandler : IRequestHandler<GetLowStockProductsQuery, List<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLowStockProductsQueryHandler> _logger;

        public GetLowStockProductsQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetLowStockProductsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ProductDto>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetLowStockProducts requested");
            var lowStock = await _uow.Repository<Product>().Query()
                .Where(p => p.CurrentStock < request.Threshold)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ProductDto>>(lowStock);
        }
    }
}
