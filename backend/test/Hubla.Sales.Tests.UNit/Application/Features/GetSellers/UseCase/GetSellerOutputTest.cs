using Hubla.Sales.Application.Features.GetSellers.UseCase;
using Hubla.Sales.Application.Shared.Sales.Entities;

namespace Hubla.Sales.Tests.Unit.Application.Features.GetSellers.UseCase
{
    public class GetSellerOutputTest
    {
        [Fact]
        public void CreateGetSellerOutput_ShouldCreate_ValidGetSellerOutput()
        {
            // Arrange
            (int id, string name, GetSellerSalesListOutput sales) data
                 = (1, "some seller", GetSellerSalesListOutput.Create(new List<Sale>()));

            // Act
            var output = GetSellerOutput.Create(data.id, data.name, data.sales);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(data.id, output.Id);
            Assert.Equal(data.name, output.Name);
        }
    }
}