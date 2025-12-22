using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.Entities.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InventorySales.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public TokenResult CreateTokens(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            var keyString = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:AccessTokenExpirationMinutes"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var randomBytes = new byte[64];
            RandomNumberGenerator.Fill(randomBytes);
            var refreshToken = Convert.ToBase64String(randomBytes);
            var refreshDaysStr = _config["Jwt:RefreshTokenExpirationDays"];
            double refreshDays = 7; // default
            if (!string.IsNullOrEmpty(refreshDaysStr)) double.TryParse(refreshDaysStr, out refreshDays);
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(refreshDays);

            return new TokenResult(accessToken, refreshToken, refreshTokenExpiration);
        }


    }
}
