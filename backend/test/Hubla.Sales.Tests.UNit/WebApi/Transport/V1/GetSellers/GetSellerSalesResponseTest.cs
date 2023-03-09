using Hubla.Sales.API.Transport.V1.GetSellers;
using Hubla.Sales.Application.Shared.Sales.Enums;

namespace Hubla.Sales.Tests.Unit.WebApi.Transport.V1.GetSellers
{
    public class GetSellerSalesResponseTest
    {
        [Fact]
        public void ShouldCreateObject()
        {
            // arrange - act
            (int id, SaleType saleType, DateTime date, string description, decimal value) data =
                (1, SaleType.ProducerSale, DateTime.Now, "some description", 125M);

            var result = new GetSellerSalesResponse(data.id, data.saleType, data.date, data.description, data.value);
            result.Id.Should().Be(data.id);
            result.SaleType.Should().Be(data.saleType);
            result.Date.Should().Be(data.date);
            result.Description.Should().Be(data.description);
            result.Value.Should().Be(data.value);
        }
    }
}