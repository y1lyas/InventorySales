using InventorySales.Application.Features.Sales.Commands.MakeSale;
using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Application.Features.Sales.Queries.GetSale;
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
        //[EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "SaleCreate")]
        [HttpPost("sales")]
        public async Task<IActionResult> MakeSale([FromBody] MakeSaleCommand request)
        {
            var saleId = await _mediator.Send(request);
            return Ok(new { saleId });
        }
        //[EnableRateLimiting("read-policy")]
        //[Authorize(Policy = "SaleReadOwn")]
        [HttpGet("sales")]
        public async Task<IActionResult> GetUserSales([FromQuery] GetAllSalesQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("sales/{id}")]
        public async Task<IActionResult> GetSale([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetSaleQuery(id));
            return Ok(result);
        }
    }
}
