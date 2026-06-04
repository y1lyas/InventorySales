using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Queries.GetAllProducts;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;

        public GetAllProductsQueryHandler(IUnitOfWork uow, IMapper mapper, IPaginationService paginationService)
        {
            _uow = uow;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
        {
            var baseQuery = _uow.Repository<Product>().Query().AsNoTracking().ApplyFilters(request);

            return await _paginationService
          .CreateAsync<Product, ProductDto>(
              baseQuery,
              q => q.OrderByDescending(x => x.CreatedDate),
              request.PageNumber,
              request.PageSize,
              _mapper.ConfigurationProvider,
              ct);
        }
    }
}
