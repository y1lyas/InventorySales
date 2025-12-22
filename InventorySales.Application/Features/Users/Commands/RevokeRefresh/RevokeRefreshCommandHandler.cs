using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace InventorySales.Application.Features.Users.Commands.RevokeRefresh
{
    public class RevokeRefreshCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userInfo;
        public RevokeRefreshCommandHandler(IUnitOfWork uow, IUserService userInfo)
        {
            _uow = uow;
            _userInfo = userInfo;
        }
        public async Task<Unit> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userId = _userInfo.UserId;

            var user = await _uow.Repository<User>().Query().Where(u => u.Id == userId)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(cancellationToken)
                     ?? throw new RefreshTokenInvalidException();

            var rt = user.RefreshTokens.Where(r => r.RevokedAt == null).FirstOrDefault();

            if (rt == null || !rt.IsActive)
                throw new RefreshTokenInvalidException();

            user.RevokeRefreshToken(rt.Token);
            return Unit.Value;
        }
    }
}
