using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Paging;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

            var query = _uow.Repository<Product>().Query().AsNoTracking();

            if (request.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId.Value);
            }

            var totalCount = await query.LongCountAsync(ct);

            var items = await query
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var totalPages = pageSize == 0 ? 0 : (int)Math.Ceiling((double)totalCount / pageSize);

            return new PagedResult<ProductDto>(items, pageNumber, pageSize, totalCount, totalPages);
        }
    }
}
