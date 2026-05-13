using InventorySales.Application.Abstractions;
using InventorySales.Application.Features.Categories.DTOs;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken ct)
        {
            var category = new Category(request.Name, request.Description);
            await _uow.Repository<Category>().AddAsync(category);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
