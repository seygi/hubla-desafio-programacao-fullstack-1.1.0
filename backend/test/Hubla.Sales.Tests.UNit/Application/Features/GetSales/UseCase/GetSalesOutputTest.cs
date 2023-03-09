using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs;

namespace Hubla.Sales.Tests.Unit.Application.Features.GetSales.UseCase
{
    public class GetSalesOutputTest
    {
        [Fact]
        public void CreateGetSalesOutput_ShouldCreate_ValidGetSalesOutput()
        {
            // Arrange
            (int id, SaleType saleType, DateTime date, string description, decimal value, SellerOutputBase seller) data 
                 = (1, SaleType.ProducerSale, DateTime.Now, "some description", 125M, GetSalesSellerOutput.Create(1, "test"));

            // Act
            var output = GetSalesOutput.Create(data.id, data.saleType, data.date, data.description, data.value, data.seller);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(data.id, output.Id);
            Assert.Equal(data.saleType, output.SaleType);
            Assert.Equal(data.date, output.Date);
            Assert.Equal(data.description, output.Description);
            Assert.Equal(data.value, output.Value);
            Assert.Equal(data.seller, output.Seller);
        }
    }
}
