using InventorySales.Application.Features.Sales.Commands.MakeSale;
using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Application.Features.Sales.Queries.GetSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace InventorySales.API.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : Controller
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Policy = "SaleCreate")]
        [HttpPost("make-sale")]
        public async Task<IActionResult> MakeSale([FromBody] MakeSaleCommand request)
        {
            var saleId = await _mediator.Send(request);
            return Ok(new { saleId });
        }
        [Authorize(Policy = "SaleReadOwn")]
        [HttpGet("sales")]
        public async Task<IActionResult> GetUserSales()
        {
            var result = await _mediator.Send(new GetSalesQuery());
            return Ok(result);
        }
    }
}
