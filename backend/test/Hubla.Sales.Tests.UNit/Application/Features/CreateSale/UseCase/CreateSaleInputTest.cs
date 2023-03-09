using Hubla.Sales.Application.Features.CreateSale.UseCase;

namespace Hubla.Sales.Tests.Unit.Application.Features.CreateSale.UseCase
{
    public class CreateSaleInputTest
    {
        [Fact]
        public void CreateCreateSaleInput_ShouldCreate_ValidCreateSaleInput()
        {
            // Arrange
            var _file = File.ReadAllBytes("sales.txt");

            // Act
            var input = CreateSaleInput.Create(_file);

            // Assert
            Assert.NotNull(input);
            Assert.Equal(input.File, _file);
        }
    }
}
