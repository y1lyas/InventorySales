using InventorySales.Application.Exceptions;
using InventorySales.Application.Interfaces;
using InventorySales.Application.Specifications;
using InventorySales.Domain.Entities.Auth;
using System.Linq;

namespace InventorySales.Application.Features.Users.Commands.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userInfo;

        public RefreshTokenCommandHandler(IUnitOfWork uow, ITokenService tokenService, IUserService userInfo)
        {
            _uow = uow;
            _tokenService = tokenService;
            _userInfo = userInfo;
        }

        public async Task<TokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userId = _userInfo.UserId;

            var spec = new UserWithRefreshTokenSpecification(userId);

            var user = await _uow.Repository<User>().GetBySpecAsync(spec)
                     ?? throw new RefreshTokenInvalidException();

            var rt = user.RefreshTokens.Where(r => r.RevokedAt == null).FirstOrDefault();

            if (rt == null || !rt.IsActive)
                    throw new RefreshTokenInvalidException();

            rt.Revoke();

            var tokens = _tokenService.CreateTokens(user);
            user.AddRefreshToken(tokens.RefreshToken, tokens.RefreshTokenExpiresAt);

            return tokens;
        }
    }
}
