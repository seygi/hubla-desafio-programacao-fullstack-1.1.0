using Hubla.Sales.Application.Features.CreateSale.UseCase;

namespace Hubla.Sales.Tests.Unit.Application.Features.CreateSale.UseCase
{
    public class CreateSaleOutputTest
    {
        public bool Success { get; }
        [Fact]
        public void CreateCreateSaleOutput_ShouldCreate_ValidCreateSaleOutput()
        {
            // Arrange

            // Act
            var output = CreateSaleOutput.Create(true);

            // Assert
            Assert.NotNull(output);
            Assert.True(output.Success);
        }
    }
}