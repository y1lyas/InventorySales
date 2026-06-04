using InventorySales.Application.Features.Categories.Commands.AssignCategory;
using InventorySales.Application.Features.Categories.Commands.CreateCategory;
using InventorySales.Application.Features.Categories.Commands.UnassignCategory;
using InventorySales.Application.Features.Categories.Queries.GetAllCategories;
using InventorySales.Application.Features.Categories.Queries.GetCategoryById;
using InventorySales.Application.Features.Products.Commands.CreateProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace InventorySales.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[EnableRateLimiting("read-policy")]
        //[Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCategoriesQuery request)
        {
            var categories = await _mediator.Send(request);
            return Ok(categories);
        }
        //[EnableRateLimiting("read-policy")]
        //[Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var category = await _mediator.Send(new GetCategoriesQuery(id));

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        //[EnableRateLimiting("write-policy")]
        //[Authorize(Policy = "CategoryCreate")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand request)
        {
            var categoryId = await _mediator.Send(request);
            return Ok(categoryId);
        }
        //[EnableRateLimiting("write-policy")]
        //[Authorize]
        [HttpPatch("assign")]
        public async Task<IActionResult> Assign([FromBody] AssignCategoryCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPatch("unassign")]
        public async Task<IActionResult> Unassign([FromBody] UnassignCategoryCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
