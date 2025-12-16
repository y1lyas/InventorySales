using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Interfaces;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Products.Queries.GetLowStock
{
    public class GetLowStockProductsQueryHandler : IRequestHandler<GetLowStockProductsQuery, List<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetLowStockProductsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
        {
            var lowStock = _uow.Repository<Product>().Query()
                .Where(p => p.CurrentStock < request.Threshold)
                .ToList();

            return _mapper.Map<List<ProductDto>>(lowStock);
        }
    }
}
