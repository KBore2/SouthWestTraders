using Microsoft.AspNetCore.Mvc;
using Moq;
using SouthWestTradersApi.Controllers;
using SouthWestTradersAPI.Domain.IRepository;
using SouthWestTradersAPI.Domain.IServices;
using SouthWestTradersAPI.Domain.Models;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SouthWestTradersAPI.Tests
{
    public class ProductControllerTest
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly Mock<IProductService> productServiceMock;
        public ProductControllerTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            this.productServiceMock = new Mock<IProductService>();

        }


        [Fact]
        public async Task GetProductByName_NoProductName_ReturnException()
        {
            //Arrange
           
           
            productServiceMock.Setup(p => p.GetProductByName(It.IsAny<string>())).ReturnsAsync((Product)null);


            var controller = new ProductController(productServiceMock.Object);

            //Act
            var result = await controller.GetProductByName("whaterv");

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public async Task GetProductByName_ProductName_ReturnsProduct()
        {
            //*/Arrange
            var productServiceStub = new Mock<IProductService>();


            productServiceStub.Setup(p => p.GetProductByName(It.IsAny<string>())).ReturnsAsync(new Product { ProductId=1,Name="Hello",Price=0});


            var controller = new ProductController(productServiceStub.Object);

            //Act
            Product product = await controller.GetProductByName("Hello");

            //Assert
            Assert.NotNull(product);
            Assert.Equal(1, product.ProductId);


            


        }
    }
}