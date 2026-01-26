using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Repositories;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Application.Features.Products.Commands.CreateProduct;
using InventorySales.Domain.Entities;
using InventorySales.Domain.Entities.Auth;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Tests.Application
{
    public class CreateProductHandlerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IUserContext> _userServiceMock;
        private readonly CreateProductCommandHandler _handler;
        public CreateProductHandlerTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _userServiceMock = new Mock<IUserContext>();

            _handler = new CreateProductCommandHandler(
                _uowMock.Object,
                _userServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_Should_Add_Product_To_Repository()
        {
            var request = new CreateProductCommand(
                 Name: "Test Product",
                 UnitPrice: 100
               );

            var productRepoMock = new Mock<IRepository<Product>>();

            _uowMock
                .Setup(x => x.Repository<Product>())
                .Returns(productRepoMock.Object);

            _userServiceMock
                .Setup(x => x.GetCurrentUserAsync(CancellationToken.None))
                .ReturnsAsync(Mock.Of<User>(u => u.ExternalId == "user-1"));

            await _handler.Handle(request, CancellationToken.None);

            productRepoMock.Verify(
                x => x.AddAsync(It.Is<Product>(p =>
                    p.Name == "Test Product" &&
                    p.UnitPrice == 100 &&
                    p.CreatedById == "user-1"
                )),
                Times.Once
            );
        }

    }
}
