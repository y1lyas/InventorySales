using InventorySales.Application.Interfaces;

namespace InventorySales.Application.Features.Users.Commands.Refresh
{
    public record RefreshTokenCommand() : IRequest<TokenResult>;
}
