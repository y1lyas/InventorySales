namespace InventorySales.Application.Abstractions.RedisCache
{
    public interface ICacheKeyGenerator
    {
        string Generate(object request);
    }
}
