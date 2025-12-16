using InventorySales.Application.Interfaces;

namespace InventorySales.Application.Features.Users.Commands.Register
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<TokenResult>;
}
