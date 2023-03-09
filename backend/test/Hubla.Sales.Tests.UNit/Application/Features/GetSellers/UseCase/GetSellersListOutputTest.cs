using Hubla.Sales.Application.Features.GetSellers.UseCase;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Tests.Unit.Application.Features.GetSellers.UseCase
{
    [ExcludeFromCodeCoverage]
    public class GetSellersListOutputTest
    {
        [Fact]
        public void CreateGetSellersListOutput_ShouldCreate_ValidGetSellersListOutput()
        {
            // Arrange
            var listSales1 = new List<Sale>()
            {
                Sale.CreateWithoutSeller(SaleType.ProducerSale, DateTime.Now, "some description 1", 125M),
                Sale.CreateWithoutSeller(SaleType.AffiliateSale, DateTime.Now, "some description 2", 250M)
            };
            var listSales2 = new List<Sale>()
            {
                Sale.CreateWithoutSeller(SaleType.ProducerSale, DateTime.Now, "some description 3", 135M),
                Sale.CreateWithoutSeller(SaleType.PaidComission, DateTime.Now, "some description 4", 5M)
            };

            var list = new List<Seller>()
            {
                Seller.Create(1, "some name 1", listSales1),
                Seller.Create(2, "some name 2", listSales2)
            };

            // Act
            var output = GetSellersListOutput.Create(list);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(2, output.Count());
            Assert.Equal(2, output.First().Sales.Count());
        }
    }
}