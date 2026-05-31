using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Products.DTOs;
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
            var baseQuery = _uow.Repository<Product>().Query().AsNoTracking();

            if (request.IsDeleted.GetValueOrDefault())
            {
                baseQuery = baseQuery.IgnoreQueryFilters().Where(p => p.IsDeleted)
            .OrderByDescending(p => p.DeletedAt);
            }
            if (request.CategoryId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.CategoryId == request.CategoryId.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.Trim().ToLower();
                baseQuery = baseQuery.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    x.Sku.Value.ToLower().Contains(search));
            }
            if (request.MinStock.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.Stock.Value >= request.MinStock.Value);
            }
            if (request.MaxStock.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.Stock.Value <= request.MaxStock.Value);
            }
            if (request.MinPrice.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.Price.Amount >= request.MinPrice.Value);
            }
            if (request.MaxPrice.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.Price.Amount <= request.MaxPrice.Value);
            }
            if (request.StartDate.HasValue)
            {
                baseQuery = baseQuery.Where(x =>
                    x.CreatedDate >= request.StartDate.Value);
            }
            if (request.EndDate.HasValue)
            {
                var endDate =
                    request.EndDate.Value.Date.AddDays(1);

                baseQuery = baseQuery.Where(x =>
                    x.CreatedDate < endDate);
            }

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
