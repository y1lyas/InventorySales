using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Application.Features.Products.Commands.DecreaseStock;
using InventorySales.Application.Features.Products.Commands.IncreaseStock;
using InventorySales.Application.Features.Products.Commands.UpdatePrice;
using InventorySales.Application.Features.Products.DTOs;
using InventorySales.Application.Features.Products.Queries.GetLowStock;
using InventorySales.Application.Features.Products.Queries.GetProducts;
using InventorySales.Domain.Entities.Auth;

namespace InventorySales.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<CreateProductCommand>(dto);
            var productId = await _mediator.Send(command);
            return Created(string.Empty, productId);
        }
        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] UpdatePriceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<UpdateProductPriceCommand>(dto);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("increase-stock")]
        public async Task<IActionResult> IncreaseStock([FromBody] IncreaseStockDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<IncreaseStockCommand>(dto);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("decrease-stock")]
        public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<DecreaseStockCommand>(dto);
            await _mediator.Send(command);
            return Ok();
        }
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 5)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new GetLowStockProductsQuery(threshold);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
