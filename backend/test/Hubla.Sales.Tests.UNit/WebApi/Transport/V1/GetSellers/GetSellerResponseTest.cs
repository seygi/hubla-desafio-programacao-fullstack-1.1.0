using Hubla.Sales.API.Transport.V1.GetSellers;

namespace Hubla.Sales.Tests.Unit.WebApi.Transport.V1.GetSellers
{
    public class GetSellerResponseTest
    {
        [Fact]
        public void ShouldCreateObject()
        {
            var sales = new List<GetSellerSalesResponse>();

            // arrange - act
            (int id, string name, IEnumerable<GetSellerSalesResponse> sales, decimal amountTotal) data =
                (1, "some name", sales, 0);

            var result = new GetSellerResponse(data.id, data.name, data.sales, data.amountTotal);
            result.Id.Should().Be(data.id);
            result.Name.Should().Be(data.name);
            result.Sales.Should().BeEquivalentTo(data.sales);
            result.AmountTotal.Should().Be(data.amountTotal);
        }
    }
}