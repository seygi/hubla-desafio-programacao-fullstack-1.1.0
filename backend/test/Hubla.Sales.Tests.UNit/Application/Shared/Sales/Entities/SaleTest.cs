using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;

namespace Hubla.Sales.Tests.Unit.Application.Shared.Sales.Entities
{
    public class SaleTest
    {
        [Fact(DisplayName = "ShouldCreateSale")]
        public void ShouldCreateSale()
        {
            // arrange
            (SaleType saleType, DateTime date, string description, decimal value) data =
                (SaleType.ProducerSale, DateTime.Now, "some description", 125M);

            // act
            var sale = Sale.CreateWithoutSeller(data.saleType, data.date, data.description, data.value);
            sale.SaleType.Should().Be(data.saleType);
            sale.Date.Should().Be(data.date);
            sale.Description.Should().Be(data.description);
            sale.Value.Should().Be(data.value);
        }
    }
}