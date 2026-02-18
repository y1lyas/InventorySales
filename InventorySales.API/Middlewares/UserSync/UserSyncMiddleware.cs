using InventorySales.Application.Abstractions;
using InventorySales.Domain.Entities.Auth;
using InventorySales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InventorySales.API.Middlewares.Auth
{
    public sealed class UserSyncMiddleware
    {
        private readonly RequestDelegate _next;

        public UserSyncMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IUnitOfWork uow)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var externalId = context.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(externalId))
                {
                    var user = await uow.Repository<User>().Query().FirstOrDefaultAsync(a => a.ExternalId == externalId );

                    if (user is null)
                    {
                        var email = context.User
                            .FindFirst(ClaimTypes.Email)?.Value;

                        user = User.Create(externalId, email);

                        await uow.Repository<User>().AddAsync(user);

                        await uow.SaveChangesAsync();
                    }
                }
            }

            await _next(context);
        }
    }

}
