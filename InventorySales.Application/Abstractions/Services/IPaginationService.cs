using InventorySales.Application.Common.Pagination;

namespace InventorySales.Application.Abstractions.Services
{
    public interface IPaginationService
    {
        Task<PagedResult<TDto>> CreateAsync<TEntity, TDto>(
        IQueryable<TEntity> baseQuery,
        Func<IQueryable<TEntity>,
        IOrderedQueryable<TEntity>> orderBy,
        int pageNumber,
        int pageSize,
        IConfigurationProvider mapperConfig,
        CancellationToken ct = default)
        where TEntity : class;
    }
}
