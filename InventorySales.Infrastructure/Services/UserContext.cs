using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.Services
{
    public class UserContext : IUserContext
    {
        private readonly IUserService _userInfo;
        private readonly IUnitOfWork _uow;
        public UserContext(IUserService userInfo, IUnitOfWork uow)
        {
            _userInfo = userInfo;
            _uow = uow;
        }
        public async Task<User> GetCurrentUserAsync(CancellationToken ct)
        {
            var userId = _userInfo.UserId ?? throw new Exception("User Id not found");
            var user = await _uow.Repository<User>().Query().FirstOrDefaultAsync(u => u.ExternalId == userId, ct);

            if (user is null)
                throw new ApplicationException("User doesn't exist");

            return user;
        }
    }
}
