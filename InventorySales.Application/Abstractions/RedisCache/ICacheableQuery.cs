using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Abstractions.RedisCache
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
        TimeSpan CacheDuration { get; }
    }
}
