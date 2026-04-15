using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Paging;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
        {
            var pageNumber = Math.Max(1, request.PageNumber);
            var pageSize = Math.Clamp(request.PageSize, 1, 200);

            var queryable = _uow.Repository<Product>().Query().AsNoTracking();

            if (request.IsDeleted.GetValueOrDefault())
            {
                queryable = queryable.IgnoreQueryFilters().Where(p => p.IsDeleted)
            .OrderByDescending(p => p.DeletedAt);
            }
            if (request.CategoryId.HasValue)
            {
                queryable = queryable.Where(x => x.CategoryId == request.CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.Trim().ToLower();
                queryable = queryable.Where(x =>
                    x.Name.ToLower().Contains(search) ||
                    x.Sku.Value.ToLower().Contains(search));
            }

            var totalCount = await queryable.LongCountAsync(ct);

            var items = await queryable
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var totalPages = pageSize == 0 ? 0 : (int)Math.Ceiling((double)totalCount / pageSize);

            return new PagedResult<ProductDto>(items, pageNumber, pageSize, totalCount, totalPages);
        }
    }
}
