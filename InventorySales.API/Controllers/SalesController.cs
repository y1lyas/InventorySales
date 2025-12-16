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
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("make-sale")]
        public async Task<IActionResult> MakeSale([FromBody] MakeSaleDto dto)
        {
            var command = _mapper.Map<MakeSaleCommand>(dto);
            var saleId = await _mediator.Send(command);
            return Ok(new { saleId });
        }
        [HttpGet("sales")]
        public async Task<IActionResult> GetUserSales()
        {
            var query = new GetSalesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
