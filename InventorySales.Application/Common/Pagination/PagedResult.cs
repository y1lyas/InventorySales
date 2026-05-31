namespace InventorySales.Application.Common.Pagination
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; init; }

        public int PageNumber { get; init; }

        public int PageSize { get; init; }

        public long TotalCount { get; init; }

        public int TotalPages { get; init; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public PagedResult(
            IReadOnlyList<T> items,
            int pageNumber,
            int pageSize,
            long totalCount,
            int totalPages)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }
    }
}
