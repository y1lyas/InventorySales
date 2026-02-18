using InventorySales.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.Services.CorrelationContext
{
    public class HttpCorrelationContext : ICorrelationContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpCorrelationContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Id
            => _httpContextAccessor.HttpContext?
                   .Items["CorrelationId"]?.ToString()
               ?? "unknown";

    }

}
