using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Exceptions;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Commands.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userService;
        public RemoveProductCommandHandler(IUnitOfWork uow,IUserService userService)
        {
            _uow = uow;
            _userService = userService;
        }
        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken ct)
        {
            var userId = _userService.UserId;
            var product = await _uow.Repository<Product>().GetByIdAsync(request.productId);

            if (product == null)
                throw new ProductNotFoundException(request.productId);

            product.SoftDelete(userId);

            return Unit.Value;
        }
    }
}
