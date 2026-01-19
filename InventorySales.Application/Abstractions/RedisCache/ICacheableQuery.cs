namespace InventorySales.Application.Abstractions.RedisCache
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
        TimeSpan CacheDuration { get; }
    }
}
