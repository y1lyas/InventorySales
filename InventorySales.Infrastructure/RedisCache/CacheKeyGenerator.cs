using InventorySales.Application.Abstractions.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.RedisCache
{
    public class CacheKeyGenerator : ICacheKeyGenerator
    {
            public string Generate(object request)
            {
                var typeName = request.GetType().Name;

            var props = request.GetType()
       .GetProperties()
       .OrderBy(p => p.Name)
       .Select(p => new { p.Name, Value = p.GetValue(request) })
       .Where(x => x.Value != null && !IsDefaultValue(x.Value))
       .ToList();

            if (!props.Any())
                return $"{typeName}:all";

            var parts = props.Select(x =>
            {
                var v = x.Value!;
                string s = v switch
                {
                    string str => str,
                    _ when v.GetType().IsValueType => v.ToString()!,
                    _ => JsonSerializer.Serialize(v)
                };
                return $"{x.Name}={Uri.EscapeDataString(s)}";
            });

            return $"{typeName}:{string.Join("&", parts)}";
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
