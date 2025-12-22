using InventorySales.Application.Abstractions.Services;
using System.ComponentModel;

namespace InventorySales.Application.Features.Users.Commands.Login
{
    public class LoginUserCommand : IRequest<TokenResult>
    {
        [DefaultValue("1@gmail.com")]
        public string Email { get; set; }
        [DefaultValue("12345678")]
        public string Password { get; set; }
    }

}
