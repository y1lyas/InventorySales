using InventorySales.Application.Features.Users.Commands.Login;
using InventorySales.Application.Features.Users.Commands.Refresh;
using InventorySales.Application.Features.Users.Commands.Register;
using InventorySales.Application.Features.Users.Commands.RevokeRefresh;
using Microsoft.AspNetCore.Authorization;

namespace InventorySales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
        {
            var tokens = await _mediator.Send(request);
            return Ok(tokens);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var tokens = await _mediator.Send(request);
            return Ok(tokens);
        }
        [Authorize]
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            var command = new RefreshTokenCommand();
            var tokens = await _mediator.Send(command);
            return Ok(tokens);
        }
        [Authorize]
        [HttpPost("Revoke")]
        public async Task<IActionResult> Revoke()
        {
            var command = new RevokeRefreshTokenCommand();
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
