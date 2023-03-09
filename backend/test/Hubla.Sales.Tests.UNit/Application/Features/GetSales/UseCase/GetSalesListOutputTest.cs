using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Tests.Unit.Application.Features.GetSales.UseCase
{
    [ExcludeFromCodeCoverage]
    public sealed class GetSalesListOutputTest
    {
        [Fact]
        public void CreateGetSalesListOutput_ShouldCreate_ValidGetSalesListOutput()
        {
            // Arrange
            var seller = Seller.Create("test");
            var list = new List<Sale>() 
            {
                Sale.Create(SaleType.ProducerSale, DateTime.Now, "some description 1", 125M, seller),
                Sale.Create(SaleType.ProducerSale, DateTime.Now, "some description 2", 125M, seller)
            };

            // Act
            var output = GetSalesListOutput.Create(list);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(2, output.Count());
        }
    }
}