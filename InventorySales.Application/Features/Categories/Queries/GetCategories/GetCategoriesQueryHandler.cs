using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions;
using InventorySales.Application.Common.Exceptions;
using InventorySales.Application.Features.Categories.DTOs;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoryDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetCategoriesQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            var categoryDto = await _uow.Repository<Category>()
         .Query()
         .AsNoTracking()
         .Where(x => x.Id == request.CategoryId)
         .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
         .FirstOrDefaultAsync(ct);

            if (categoryDto == null)
                throw new CategoryNotFoundException(request.CategoryId);

            return categoryDto;

        }
    }
}
