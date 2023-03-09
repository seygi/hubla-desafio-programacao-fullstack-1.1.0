using Hubla.Sales.Application.Features.GetSellers.UseCase;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sellers.Entities;

namespace Hubla.Sales.Tests.Unit.Application.Features.GetSellers.UseCase
{
    public class GetSellerSalesListOutputTest
    {
        [Fact]
        public void CreateGetSellerSalesListOutputTest_ShouldCreate_ValidGetSellerSalesListOutput()
        {
            // Arrange
            var seller = Seller.Create("test");
            var list = new List<Sale>()
            {
                Sale.Create(SaleType.ProducerSale, DateTime.Now, "some description 1", 125M, seller),
                Sale.Create(SaleType.ProducerSale, DateTime.Now, "some description 2", 125M, seller)
            };

            // Act
            var output = GetSellerSalesListOutput.Create(list);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(2, output.Count());
        }
    }
}
