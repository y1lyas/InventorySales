using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Features.Categories.DTOs;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Paging;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<PagedResult<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
        {
            var pageNumber = Math.Max(1, request.PageNumber);
            var pageSize = Math.Clamp(request.PageSize, 1, 200);
            var queryable = _uow.Repository<Category>().Query().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.categoryName))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(request.categoryName));
            }

            queryable = queryable.OrderByDescending(x => x.CreatedDate);
            var totalCount = await queryable.CountAsync(ct);


            var items = await queryable
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<CategoryDto>(
     items,
     pageNumber,
     pageSize,
     totalCount,
     totalPages
 );
        }
    }
}
