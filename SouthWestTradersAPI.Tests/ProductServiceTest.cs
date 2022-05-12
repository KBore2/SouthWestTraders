/*using Moq;
using SouthWestTradersAPI.BusinessLogic.Services;
using SouthWestTradersAPI.Domain.IRepository;
using SouthWestTradersAPI.Domain.IServices;
using SouthWestTradersAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SouthWestTradersAPI.Tests
{
    public class ProductServiceTest
    {

        private readonly Mock<IProductRepository> ProductRepositoryMock;
        private readonly Mock<IStockService> stockServiceMock;

        public ProductServiceTest()
        {
            ProductRepositoryMock = new Mock<IProductRepository>();
            stockServiceMock = new Mock<IStockService>();
        }

        [Fact]
        public async Task GetProductByName_NoProduct_ReturnsNull()
        {
            //aarange
            ProductRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync((Product)null);

            var product = new ProductService(ProductRepositoryMock.Object,stockServiceMock.Object);

            //act
            var result = await product.GetProductByName()

            ProductRepositoryMock.Verify(s => s.GetAsync(s => s.ProductId == 26));

            //assert
            Assert.Null(result);


        }

        [Fact]
        public async Task GetProductfForProduct_Product_ReturnsProduct()
        {
            //aarange
            ProductRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(new Product { ProductId = 1, ProductId = 1, AvailableProduct = 0 });

            var service = new ProductService(ProductRepositoryMock.Object);

            //act
            var result = await service.GetProductfForProduct(1);

            ProductRepositoryMock.Verify(s => s.GetAsync(s => s.ProductId == 1));

            //assert
            Assert.NotNull(result);
            Assert.Equal(0, result.AvailableProduct);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.ProductId);


        }

    }
}
*/