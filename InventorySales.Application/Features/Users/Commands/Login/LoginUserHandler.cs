using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities.Auth;

namespace InventorySales.Application.Features.Users.Commands.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, TokenResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        public LoginUserHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }
        public async Task<TokenResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Email == request.Email);
            if (user == null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                throw new EmailOrPasswordInvalidException();
            }
            var tokens = _tokenService.CreateTokens(user);

            user.AddRefreshToken(tokens.RefreshToken, tokens.RefreshTokenExpiresAt);

            return tokens;
        }
    }
}
