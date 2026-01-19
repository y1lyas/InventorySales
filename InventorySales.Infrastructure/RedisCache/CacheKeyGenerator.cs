using InventorySales.Application.Abstractions.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.RedisCache
{
    public class CacheKeyGenerator : ICacheKeyGenerator
    {
            public string Generate(object request)
            {
                var typeName = request.GetType().Name;

            var properties = request.GetType()
                .GetProperties()
                .OrderBy(p => p.Name)
                .ToDictionary(
                    p => p.Name,
                    p => p.GetValue(request)
                );

            var relevantProps = properties
               .Where(kvp => kvp.Value != null &&
                            !IsDefaultValue(kvp.Value))
               .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            if (relevantProps.Count == 0)
            {
                return $"{typeName}:all";
            }

            return $"{typeName}:{string.Join("_", properties)}";
            }

        private bool IsDefaultValue(object value)
        {
            if (value == null) return true;

            var type = value.GetType();

            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                return value.Equals(defaultValue);
            }

            if (type == typeof(string))
                return string.IsNullOrWhiteSpace((string)value);

            return false;
        }
    }
}
