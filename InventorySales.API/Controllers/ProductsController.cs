using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.Commands.DecreaseStock;
using InventorySales.Application.Features.Products.Commands.IncreaseStock;
using InventorySales.Application.Features.Products.Commands.UpdatePrice;
using InventorySales.Application.Features.Products.Queries.GetLowStock;
using InventorySales.Application.Features.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace InventorySales.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Policy = "ProductRead")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }
        [EnableRateLimiting("fixed")]
        [Authorize(Policy = "ProductCreate")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var productId = await _mediator.Send(request);
            return Created(string.Empty, productId);
        }
        [Authorize(Policy = "ProductUpdatePrice")]
        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] UpdateProductPriceCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        [Authorize(Policy = "StockIncrease")]
        [HttpPost("increase-stock")]
        public async Task<IActionResult> IncreaseStock([FromBody] IncreaseStockCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [Authorize(Policy = "StockDecrease")]
        [HttpPost("decrease-stock")]
        public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockCommand request)
        {
                await _mediator.Send(request);
            return Ok();
        }
        [Authorize(Policy = "StockReadLow")]
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 5)
        {
            var query = new GetLowStockProductsQuery(threshold);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
