using InventorySales.Application.Abstractions;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.UnassignCategory
{
    public class UnassignCategoryCommandHandler : IRequestHandler<UnassignCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        public UnassignCategoryCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(UnassignCategoryCommand request, CancellationToken ct)
        {
            var product = await _uow.Repository<Product>().GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new CategoryNotFoundException(request.ProductId);
            }
            product.UnassignCategory();
            return Unit.Value;
        }
    }
}