 using InventorySales.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace InventorySales.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string DefaultUserId = "00000000-0000-0000-0000-000000000001";
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId =>
      _httpContextAccessor.HttpContext?
          .User?
          .FindFirst(ClaimTypes.NameIdentifier)?
          .Value ?? DefaultUserId;

        public string? Email =>
        _httpContextAccessor.HttpContext?
             .User?
             .FindFirst(ClaimTypes.Email)?
             .Value;

        public bool IsAuthenticated => true;
    }
}
