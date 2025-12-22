using InventorySales.Domain.Entities.Auth;

namespace InventorySales.Application.Abstractions.Services
{
    public record TokenResult(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiresAt);
    public interface ITokenService
    {
        TokenResult CreateTokens(User user);
    }
}
