using InventorySales.Application.Features.Dashboard.Queries;
using Microsoft.AspNetCore.Mvc;

namespace InventorySales.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result =
                await _mediator.Send(new GetDashboardQuery());

            return Ok(result);
        }
    }
}
