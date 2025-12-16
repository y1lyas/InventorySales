using InventorySales.Application.Exceptions;
using InventorySales.Application.Interfaces;
using InventorySales.Domain.Entities.Auth;

namespace InventorySales.Application.Features.Users.Commands.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, TokenResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        public RegisterUserHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }
        public async Task<TokenResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingEmail = await _unitOfWork.Repository<User>().GetAsync(u => u.Email == request.Email);

            if (existingEmail != null)
                throw new EmailAlreadyRegisteredException(request.Email);

            var hashedPassword = _passwordHasher.Hash(request.Password);
            var user = new User();
            user.Register(request.Email, hashedPassword);
            user.AddRole(new Role("User"));

            await _unitOfWork.Repository<User>().AddAsync(user);
            var tokens = _tokenService.CreateTokens(user);
            user.AddRefreshToken(tokens.RefreshToken, tokens.RefreshTokenExpiresAt);
            return tokens;
        }
    }
}
