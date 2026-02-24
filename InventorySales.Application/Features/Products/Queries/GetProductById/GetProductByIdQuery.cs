using InventorySales.Application.Attributes;
using InventorySales.Application.Features.Interfaces;
using InventorySales.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetProductById
{
    [Cacheable(60, "Products")]
    public sealed record GetProductByIdQuery(Guid ProductId) : IRequest<ProductDto>, IQuery;
}
