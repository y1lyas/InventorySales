using InventorySales.Application.Features.Users.Commands.Login;
using InventorySales.Application.Features.Users.Commands.Refresh;
using InventorySales.Application.Features.Users.Commands.Register;
using InventorySales.Application.Features.Users.Commands.RevokeRefresh;
using InventorySales.Application.Features.Users.DTOs;

namespace InventorySales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<RegisterUserCommand>(dto);
            var tokens = await _mediator.Send(command);
            return Ok(tokens);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<LoginUserCommand>(dto);
            var tokens = await _mediator.Send(command);
            return Ok(tokens);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(CancellationToken ct)
        {
            var command = new RefreshTokenCommand();
            var tokens = await _mediator.Send(command);
            return Ok(tokens);
        }
        [HttpPost("Revoke")]
        public async Task<IActionResult> Revoke(CancellationToken ct)
        {
            var command = new RevokeRefreshTokenCommand();
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
