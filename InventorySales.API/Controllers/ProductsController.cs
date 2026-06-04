using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.Commands.DecreaseStock;
using InventorySales.Application.Features.Products.Commands.IncreaseStock;
using InventorySales.Application.Features.Products.Commands.RemoveProduct;
using InventorySales.Application.Features.Products.Commands.UpdateName;
using InventorySales.Application.Features.Products.Commands.UpdatePrice;
using InventorySales.Application.Features.Products.Queries.GetAllStockMovements;
using InventorySales.Application.Features.Products.Queries.GetLowStock;
using InventorySales.Application.Features.Products.Queries.GetProductById;
using InventorySales.Application.Features.Products.Queries.GetProducts;
using InventorySales.Application.Features.Products.Queries.GetStockMovement;
using InventorySales.Domain.Entities;
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
        //[EnableRateLimiting("read-policy")]
        //[Authorize(Policy = "ProductRead")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        //[EnableRateLimiting("read-policy")]
        //[Authorize(Policy = "ProductRead")]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetProducts([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetProductsQuery(id));
            return Ok(result);
        }
        //[EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "ProductCreate")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var productId = await _mediator.Send(request);
            return Created(string.Empty, productId);
        }
        //[EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "ProductUpdatePrice")]
        [HttpPatch("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] UpdateProductPriceCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        [HttpPatch("name")] 
        //[EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "ProductUpdateName")]
        public async Task<IActionResult> UpdateName([FromBody] UpdateProductNameCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
        //[EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "StockIncrease")]
        [HttpPost("increase-stock")]
        public async Task<IActionResult> IncreaseStock([FromBody] IncreaseStockCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        //[EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "StockDecrease")]
        [HttpPost("decrease-stock")]
        public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        //[EnableRateLimiting("read-policy")]
        //[Authorize(Policy = "StockReadLow")]
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock([FromQuery] GetLowStockProductsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        //[Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> RemoveProduct([FromQuery] RemoveProductCommand query)
        {
            await _mediator.Send(query);
            return NoContent();
        }
        [HttpGet("stock-movements")]
        public async Task<IActionResult> GetStockMovements([FromQuery] GetStockMovementsQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [HttpGet("stock-movements-all")]
        public async Task<IActionResult> GetAllStockMovements([FromQuery] GetAllStockMovementsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
