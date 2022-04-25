using Moq;
using SouthWestTradersAPI.Domain.IRepository;
using System;
using Xunit;

namespace SouthWestTradersAPI.Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public void GetProductByName_NoProductName_ReturnException()
        {
            var repositoryMock = new Mock<IProductRepository>();

            

        }
    }
}