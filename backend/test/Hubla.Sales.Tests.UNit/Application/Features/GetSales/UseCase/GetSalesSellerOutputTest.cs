using Hubla.Sales.Application.Features.GetSales.UseCase;

namespace Hubla.Sales.Tests.Unit.Application.Features.GetSales.UseCase
{
    public class GetSalesSellerOutputTest
    {

        [Fact]
        public void CreateGetSalesSellerOutput_ShouldCreate_ValidGetSalesSellerOutput()
        {
            // Arrange
            (int id, string name) data
                 = (1, "some name");

            // Act
            var output = GetSalesSellerOutput.Create(data.id, data.name);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(data.id, output.Id);
            Assert.Equal(data.name, output.Name);
        }
    }

}