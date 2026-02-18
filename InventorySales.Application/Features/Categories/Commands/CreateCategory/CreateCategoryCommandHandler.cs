using InventorySales.Application.Abstractions;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        public CreateCategoryCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken ct)
        {
            var category = new Category(request.Name, request.Description);
            await _uow.Repository<Category>().AddAsync(category);
            return category.Id;
        }
    }
}
