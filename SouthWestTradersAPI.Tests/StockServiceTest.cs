using Moq;
using SouthWestTradersAPI.BusinessLogic.Services;
using SouthWestTradersAPI.Domain.IRepository;
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
    public class StockServiceTest
    {

        private readonly Mock<IStockRepository> stockRepositoryMock;

        public StockServiceTest()
        {
            stockRepositoryMock = new Mock<IStockRepository>();
        }

        [Fact]
        public async Task GetStockfForProduct_NoProduct_ReturnsNull()
        {
            //aarange
            stockRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Stock, bool>>>())).ReturnsAsync((Stock)null);

            var service = new StockService(stockRepositoryMock.Object);

            //act
            var result = await service.GetStockfForProduct(26);

            stockRepositoryMock.Verify(s => s.GetAsync(s => s.ProductId == 26));

            //assert
            Assert.Null(result);


        }

        [Fact]
        public async Task GetStockfForProduct_Product_ReturnsStock()
        {
            //aarange
            stockRepositoryMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Stock, bool>>>())).ReturnsAsync(new Stock { StockId=1,ProductId =1,AvailableStock=0});

            var service = new StockService(stockRepositoryMock.Object);

            //act
            var result = await service.GetStockfForProduct(1);

            stockRepositoryMock.Verify(s => s.GetAsync(s => s.ProductId == 1));

            //assert
            Assert.NotNull(result);
            Assert.Equal(0, result.AvailableStock);
            Assert.Equal(1, result.StockId);
            Assert.Equal(1, result.ProductId);


        }

        [Fact]
        public async Task AddStock_ProductIdNull_ReturnsNull()
        {
            //aarange
            stockRepositoryMock.Setup(s => s.AddAsync(It.IsAny<Stock>())).ReturnsAsync(null as Stock);

            var service = new StockService(stockRepositoryMock.Object);

            //act
            var stock = new Stock { StockId = 1, AvailableStock = 0 };
            var result = await service.AddStock(stock);



            //assert
            stockRepositoryMock.Verify(s => s.AddAsync(stock));
            Assert.Null(result);
           


        }

    }
}
