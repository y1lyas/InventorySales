using InventorySales.Application.Abstractions;
using InventorySales.Application.Common.Exceptions;
using InventorySales.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Commands.UpdateName
{
    public class UpdateProductNameCommandHandler : IRequestHandler<UpdateProductNameCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        public UpdateProductNameCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(UpdateProductNameCommand request, CancellationToken ct)
        {
            var product = await _uow.Repository<Domain.Entities.Product>().GetByIdAsync(request.ProductId)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.UpdateName(request.Name);

            return Unit.Value;
        }
    }
}
