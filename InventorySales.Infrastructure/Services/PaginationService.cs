using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Common.Pagination;
using Microsoft.EntityFrameworkCore;

public class PaginationService : IPaginationService
{
    public async Task<PagedResult<TDto>> CreateAsync<TEntity, TDto>(
        IQueryable<TEntity> baseQuery,
        Func<IQueryable<TEntity>,
        IOrderedQueryable<TEntity>> orderBy,
        int pageNumber,
        int pageSize,
        AutoMapper.IConfigurationProvider mapperConfig,
        CancellationToken ct = default)
        where TEntity : class
    {
        // SAFETY
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Clamp(pageSize, 1, 200);

        // COUNT
        var totalCount = await baseQuery.CountAsync(ct);

        // ORDER + PAGINATION
        var pagedQuery = orderBy(baseQuery)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        // PROJECTION EN SON
        var items = await pagedQuery
            .ProjectTo<TDto>(mapperConfig)
            .ToListAsync(ct);

        // TOTAL PAGES
        var totalPages = (int)Math.Ceiling(
            totalCount / (double)pageSize);

        return new PagedResult<TDto>(
            items,
            pageNumber,
            pageSize,
            totalCount,
            totalPages);
    }
}