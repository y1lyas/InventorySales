namespace InventorySales.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CacheableAttribute : Attribute
    {
        public TimeSpan TtlSeconds { get; }
        public string[] Tags { get; }

        public CacheableAttribute(int ttlSeconds, params string[] tags)
        {
            TtlSeconds = TimeSpan.FromSeconds(ttlSeconds);
            Tags = tags;
        }
    }
}
