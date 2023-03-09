using Hubla.Sales.API.Transport.V1.GetSales;
using Hubla.Sales.Application.Shared.Sales.Enums;

namespace Hubla.Sales.Tests.Unit.WebApi.Transport.V1.GetSales
{
    public class GetSalesResponseTest
    {
        [Fact]
        public void ShouldCreateObject()
        {
            // arrange - act
            (int id, SaleType saleType, DateTime date, string description, decimal value, GetSalesSellerResponse seller) data =
                (1, SaleType.ProducerSale, DateTime.Now, "some description", 125M, GetSalesSellerResponse.Create(1, "test"));

            var result = new GetSalesResponse(data.id, data.saleType, data.date, data.description, data.value, data.seller);
            result.Id.Should().Be(data.id);
            result.SaleType.Should().Be(data.saleType);
            result.Date.Should().Be(data.date);
            result.Description.Should().Be(data.description);
            result.Value.Should().Be(data.value);
            result.Seller.Should().Be(data.seller);
        }
    }
}