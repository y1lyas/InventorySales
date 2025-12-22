using InventorySales.Application.Abstractions.Services;

namespace InventorySales.Application.Features.Users.Commands.Refresh
{
    public record RefreshTokenCommand() : IRequest<TokenResult>;
}
