using InventorySales.Application.Interfaces;

namespace InventorySales.Application.Features.Users.Commands.Login
{
    public record LoginUserCommand(string Email, string Password) : IRequest<TokenResult>;

}
