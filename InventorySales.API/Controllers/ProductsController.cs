using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.Commands.DecreaseStock;
using InventorySales.Application.Features.Products.Commands.IncreaseStock;
using InventorySales.Application.Features.Products.Commands.RemoveProduct;
using InventorySales.Application.Features.Products.Commands.UpdatePrice;
using InventorySales.Application.Features.Products.Queries.GetLowStock;
using InventorySales.Application.Features.Products.Queries.GetProductById;
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
        [EnableRateLimiting("read-policy")]
        //[Authorize(Policy = "ProductRead")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts(Guid? categoryId)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(categoryId));
            return Ok(result);
        }
        [EnableRateLimiting("read-policy")]
        //[Authorize(Policy = "ProductRead")]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(productId));
            return Ok(result);
        }
        [EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "ProductCreate")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var productId = await _mediator.Send(request);
            return Created(string.Empty, productId);
        }
        [EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "ProductUpdatePrice")]
        [HttpPatch("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] UpdateProductPriceCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        [EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "StockIncrease")]
        [HttpPost("increase-stock")]
        public async Task<IActionResult> IncreaseStock([FromBody] IncreaseStockCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "StockDecrease")]
        [HttpPost("decrease-stock")]
        public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [EnableRateLimiting("read-policy")]
        //[Authorize(Policy = "StockReadLow")]
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 5)
        {
            var query = new GetLowStockProductsQuery(threshold);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        //[Authorize]
        [HttpDelete("productId")]
        public async Task<IActionResult> RemoveProduct(Guid productId)
        {
            await _mediator.Send(new RemoveProductCommand(productId));
            return NoContent();
        }
    }
}
