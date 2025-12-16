using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.Commands.DecreaseStock;
using InventorySales.Application.Features.Products.Commands.IncreaseStock;
using InventorySales.Application.Features.Products.Commands.UpdatePrice;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Queries.GetLowStock;
using InventorySales.Application.Features.Products.Queries.GetProducts;

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

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            var productId = await _mediator.Send(new CreateProductCommand(dto.Name, dto.UnitPrice));
            return Created(string.Empty, productId);
        }
        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] UpdatePriceDto dto)
        {
            await _mediator.Send(new UpdateProductPriceCommand(dto.ProductId, dto.NewPrice));
            return NoContent();
        }

        [HttpPost("increase-stock")]
        public async Task<IActionResult> IncreaseStock([FromBody] IncreaseStockDto dto)
        {
            await _mediator.Send(new IncreaseStockCommand(dto.ProductId,dto.Quantity));
            return Ok();
        }

        [HttpPost("decrease-stock")]
        public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockDto dto)
        {
                await _mediator.Send(new DecreaseStockCommand(dto.ProductId, dto.Quantity));
            return Ok();
        }
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 5)
        {
            var query = new GetLowStockProductsQuery(threshold);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
