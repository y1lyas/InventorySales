using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Pagination;
using InventorySales.Application.Features.Categories.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;
        public GetAllCategoriesQueryHandler(IUnitOfWork uow, IMapper mapper, IPaginationService paginationService)
        {
            _uow = uow;
            _mapper = mapper;
            _paginationService = paginationService;
        }
        public async Task<PagedResult<CategoryDto>> Handle(
     GetAllCategoriesQuery request,
     CancellationToken ct)
        {
            var baseQuery = _uow.Repository<Category>()
                .Query()
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.categoryName))
            {
                var search = request.categoryName.Trim().ToLower();

                baseQuery = baseQuery.Where(x =>
                    x.Name.ToLower().Contains(search));
            }

            return await _paginationService
           .CreateAsync<Category, CategoryDto>(
               baseQuery,
               q => q.OrderByDescending(x => x.CreatedDate),
               request.PageNumber,
               request.PageSize,
               _mapper.ConfigurationProvider,
               ct);
        }
    }
}
