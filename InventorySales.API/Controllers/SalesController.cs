using InventorySales.Application.Features.Sales.Commands.MakeSale;
using InventorySales.Application.Features.Sales.DTOs;
using InventorySales.Application.Features.Sales.Queries.GetSales;

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

        [HttpPost("make-sale")]
        public async Task<IActionResult> MakeSale([FromBody] MakeSaleDto dto)
        {
            var saleId = await _mediator.Send(new MakeSaleCommand(dto.ProductId, dto.Quantity));
            return Ok(new { saleId });
        }
        [HttpGet("sales")]
        public async Task<IActionResult> GetUserSales()
        {
            var result = await _mediator.Send(new GetSalesQuery());
            return Ok(result);
        }
    }
}
