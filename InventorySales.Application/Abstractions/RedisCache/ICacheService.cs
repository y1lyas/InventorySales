namespace InventorySales.Application.Abstractions.RedisCache
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string cacheKey);
        Task SetAsync<T>(string cacheKey, T value, TimeSpan ttl, params string[] tags);
        Task RemoveAsync(string cacheKey);
        Task RemoveByTagAsync(string tag);

    }
}
