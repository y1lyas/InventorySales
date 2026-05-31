using InventorySales.Application.Abstractions;
using InventorySales.Application.Common.Exceptions;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Categories.Commands.AssignCategory
{
    public class AssignCategoryCommandHandler : IRequestHandler<AssignCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public AssignCategoryCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(AssignCategoryCommand request, CancellationToken ct)
        {
            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException(request.ProductId);
            }
            var category = await _uow.Repository<Category>().GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.CategoryId);
            }
            product.AssignCategory(category.Id);

            return Unit.Value;
        }

    }
}
